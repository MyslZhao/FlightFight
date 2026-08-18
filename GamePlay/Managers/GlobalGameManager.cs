using System.Collections.Generic;
using UnityEngine;

using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Handlers;
using FlightFight.Shared.Enums;
using FlightFight.Shared.Data;
using FlightFight.UI.Managers;
using System;

namespace FlightFight.GamePlay.Managers
{
    [RequireComponent(typeof(BulletManager))]
    public class GlobalGameManager: MonoBehaviour
    {
        [Header("对象管理")]
        // 弃用错误 = 强制安全
        [Obsolete("请使用 '_Planes' 字典间接使用 'PlanePropertiesHandler'")]
        [SerializeField] private PlanePropertiesHandler _Player;

        [Obsolete("请使用 '_Planes' 字典间接使用 'PlanePropertiesHandler'")]
        [SerializeField] private PlanePropertiesHandler _Enemy;

        [Header("UI管理")]

        [SerializeField] private InfoPanelManager _InfoManager;

        private BulletManager _BulletManager;

        private Dictionary<PlaneIdentity, PlanePropertiesHandler> _Planes = new();

        private Dictionary<PlaneIdentity, Transform> _Transforms = new();

        private Dictionary<PlaneIdentity, Controller> _Controllers = new();

        void Awake()
        {
            // 对象初始化

            _SafeDictionalize();

            _BulletManager = GetComponent<BulletManager>();

            var _playerCtl = _Planes[PlaneIdentity.SELF].GetComponent<Controller>();
            var _enemyCtl = _Planes[PlaneIdentity.ENEMY].GetComponent<Controller>();

            if (_playerCtl && _enemyCtl)
            {
                _playerCtl.SetGameManager(this);
                _enemyCtl.SetGameManager(this);
            }
            else
            {
                Debug.LogError("Plz check your Controllers/PropertiesHandlers if they're deployed.");
                return;
            }

            _Controllers.Add(PlaneIdentity.SELF, _playerCtl);
            _Controllers.Add(PlaneIdentity.ENEMY, _enemyCtl);

            _Transforms.Add(PlaneIdentity.SELF,
                _Planes[PlaneIdentity.SELF].GetComponent<Transform>());
            _Transforms.Add(PlaneIdentity.ENEMY,
                _Planes[PlaneIdentity.ENEMY].GetComponent<Transform>());


            // UI初始化
            if (_InfoManager)
            {
                _InfoManager.Init(
                    _Planes[PlaneIdentity.SELF].Health,
                    _Planes[PlaneIdentity.SELF].Energy,
                    _Planes[PlaneIdentity.ENEMY].Health,
                    _Planes[PlaneIdentity.ENEMY].Energy);
            }
            else
            {
                Debug.LogError("Plz check your InfoUI if its empty.");
            }
        }

        private void _SafeDictionalize()
        {
#pragma warning disable CS0612
#pragma warning disable CS0618
            if (_Player && _Enemy)
            {
                // For Safety
                _Planes.Add(_Player.Identity, _Player);
                _Planes.Add(_Enemy.Identity, _Enemy);
                _Player = null;
                _Enemy = null;
            }
            else
            {
                Debug.LogError("Plz check your kellner if its empty.");
                return;
            }
#pragma warning restore CS0612
#pragma warning restore CS0618
        }

        // 对外API
        public Vector2 GetFaceVector() => 
            (_Transforms[PlaneIdentity.SELF].position - _Transforms[PlaneIdentity.ENEMY].position).normalized;

        public void OnShoot(PlaneIdentity identity)
        {
            var _1 = _Planes[identity];
            var _2 = _Transforms[identity];


            if (_BulletManager.Shoot(_1, _2.position, _2.rotation))
            {
                _1.ConsumeAmmo();
                _1.ConsumeEnergy();
                _InfoManager.SetInfoTo(new InfoData(identity, InfoEnum.ENERGY),
                    _1.Energy);
            }
        }

        public void OnHit(PlaneIdentity identity, float damage)
        {
            _InfoManager.SetInfoBy(new InfoData(identity, InfoEnum.HEALTH), -damage);
        }
    }

}
