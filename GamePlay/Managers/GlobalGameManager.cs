using System.Collections.Generic;
using UnityEngine;

using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Handlers;
using FlightFight.Shared.Enums;
using FlightFight.Shared.Data;
using FlightFight.UI.Managers;
using System;
using System.Runtime.CompilerServices;

namespace FlightFight.GamePlay.Managers
{
    [RequireComponent(typeof(BulletManager))]
    public class GlobalGameManager: MonoBehaviour
    {
        #region 私有字段与序列字段

        [Header("对象管理")]
        // 弃用错误 = 强制安全
        [Obsolete("请使用 '_Planes' 字典间接使用 'PlanePropertiesHandler'")]
        [SerializeField] private PlanePropertiesHandler _Player;

        [Obsolete("请使用 '_Planes' 字典间接使用 'PlanePropertiesHandler'")]
        [SerializeField] private PlanePropertiesHandler _Enemy;

        [Header("UI管理")]

        [SerializeField] private InfoPanelManager _InfoManager;

        private static GlobalGameManager _Instance;

        private BulletManager _BulletManager;

        private Dictionary<PlaneIdentity, PlanePropertiesHandler> _Planes = new();

        private Dictionary<PlaneIdentity, Transform> _Transforms = new();

        private Dictionary<PlaneIdentity, Controller> _Controllers = new();

        #endregion

        #region 声明周期 

        void Awake()
        {
            // 单例模式

            if (_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // 对象初始化

            _SafeDictionalize();

            _BulletManager = GetComponent<BulletManager>();

            _Controllers.Add(PlaneIdentity.SELF, 
                _Planes[PlaneIdentity.SELF].GetComponent<Controller>());
            _Controllers.Add(PlaneIdentity.ENEMY, 
                _Planes[PlaneIdentity.ENEMY].GetComponent<Controller>());

            _Transforms.Add(PlaneIdentity.SELF,
                _Planes[PlaneIdentity.SELF].GetComponent<Transform>());
            _Transforms.Add(PlaneIdentity.ENEMY,
                _Planes[PlaneIdentity.ENEMY].GetComponent<Transform>());

            // UI初始化

            if (_InfoManager)
            {
                _InfoManager.Init(
                    _Planes[PlaneIdentity.SELF].MaxHealth,
                    _Planes[PlaneIdentity.SELF].MaxEnergy,
                    _Planes[PlaneIdentity.ENEMY].MaxHealth,
                    _Planes[PlaneIdentity.ENEMY].MaxEnergy);
            }
            else
            {
                Debug.LogError("Plz check your InfoUI if its empty.");
            }
        }

        #endregion

        #region 数据处理

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

        private Vector2 _GetFaceVector() => 
            - (_Transforms[PlaneIdentity.SELF].position - _Transforms[PlaneIdentity.ENEMY].position).normalized;

        #endregion

        #region 事件处理

        private void _OnShoot(PlaneIdentity identity)
        {
            var _1 = _Planes[identity];
            var _2 = _Transforms[identity];


            if (_BulletManager.Shoot(_1, _2.position, _2.rotation))
            {
                _1.TryShoot();
                _InfoManager.SetInfoTo(new InfoData(identity, InfoEnum.ENERGY),
                    _1.Energy);
            }
        }

        private void _OnHit(PlaneIdentity identity, float damage)
        {
            //_InfoManager.SetInfoBy(new InfoData(identity, InfoEnum.HEALTH), -damage);
            //逻辑待定
        }

        #endregion

        #region 对外API

        public static void UpdateEnergy(PlaneIdentity identity, float energy) =>
            _Instance._InfoManager.SetInfoTo(new InfoData(identity, InfoEnum.ENERGY)
                , energy);

        public static Vector2 GetFaceVector() =>
            _Instance._GetFaceVector();

        public static void OnShoot(PlaneIdentity identity) =>
            _Instance._OnShoot(identity);

        public static void OnHit(PlaneIdentity identity, float damage) =>
            _Instance._OnHit(identity, damage);

        #endregion
    }

}
