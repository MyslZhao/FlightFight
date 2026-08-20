using System.Collections.Generic;
using UnityEngine;

using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Handlers;
using FlightFight.Shared.Enums;
using FlightFight.Shared.Data;
using FlightFight.UI.Managers;
using System;
using System.Runtime.CompilerServices;
using FlightFight.UI.Manager;

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

        [SerializeField] private ValueInfoManager _ValueInfoManager;

        [SerializeField] private AmmoInfoManager _AmmoInfoManager;

        private static GlobalGameManager _Instance;

        private BulletManager _BulletManager;

        private readonly Dictionary<PlaneIdentity, PlanePropertiesHandler> _Planes = new();

        private readonly Dictionary<PlaneIdentity, Transform> _Transforms = new();

        private readonly Dictionary<PlaneIdentity, Controller> _Controllers = new();

        #endregion

        #region 生命周期 

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

            if (_ValueInfoManager)
            {
                _ValueInfoManager.Init(
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
            var _cache = _2.position + _2.up * 0.2f;

            if (_BulletManager.Shoot(_1, _cache, _2.rotation))
            {
                _1.TryShoot();
                _ValueInfoManager.SetInfoTo(new ValueInfoData(identity, ValueInfoEnum.ENERGY),
                    _1.Energy);
            }
        }

        private void _OnHit(PlaneIdentity identity, AmmoEnum bullet)
        {
            var _1 = BulletManager.BulletAssets[bullet].Damage;

            _Planes[identity].TryCauseDamage(_1);
            _ValueInfoManager.SetInfoTo(new ValueInfoData(identity, ValueInfoEnum.HEALTH),
                _Planes[identity].Health);
        }

        #endregion

        #region 对外API

        internal static void UpdateAmmo(PlaneIdentity identity, AmmoEnum newAmmo) =>
            _Instance._AmmoInfoManager.SetAmmoTo(identity,
                BulletManager.BulletAssets[newAmmo].IconSprites[identity]);

        internal static void UpdateEnergy(PlaneIdentity identity, float energy) =>
            _Instance._ValueInfoManager.SetInfoTo(new ValueInfoData(identity, ValueInfoEnum.ENERGY),
                energy);

        // 考虑未来是否用字典取代
        internal static Vector2 GetFaceTo(PlaneIdentity identity, Vector3 location) =>
            (identity) switch
            {
                PlaneIdentity.SELF =>
                    _Instance._GetFaceTo(location, _Instance._Transforms[PlaneIdentity.ENEMY].position),
                PlaneIdentity.ENEMY =>
                    _Instance._GetFaceTo(location, _Instance._Transforms[PlaneIdentity.SELF].position),
                PlaneIdentity.NONE =>
                    Vector2.zero,
                PlaneIdentity =>
                    Vector2.zero
            };


        internal static void OnShoot(PlaneIdentity identity) =>
            _Instance._OnShoot(identity);

        internal static void OnHit(PlaneIdentity plane, AmmoEnum bullet) =>
            _Instance._OnHit(plane, bullet);

        internal static void OnMiss(GameObject bullet) =>
            _Instance._BulletManager.Dismiss(bullet);

        #endregion
    }

}
