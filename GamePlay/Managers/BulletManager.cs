using System.Collections.Generic;
using Audune.Utils.Dictionary;
using FlightFight.GamePlay.Ammo;
using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Handlers;
using FlightFight.Shared.Data;
using FlightFight.Shared.DataAssets;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.GamePlay.Managers
{
    [RequireComponent(typeof(GlobalGameManager))]
    public class BulletManager: MonoBehaviour
    {
        #region 私有与序列字段

        [Header("基础")]
        [SerializeField] private SerializableDictionary<AmmoEnum, BulletAssetData> _BulletAssets;

        [SerializeField] private GameObject _BulletObject;

        [Space]
        [Header("贴图")]

        [SerializeField] private SerializableDictionary<PlaneIdentity, Sprite> _BulletSprites;

        private static BulletManager _Instance;

        #endregion

        #region 公开字段

        internal static IReadOnlyDictionary<AmmoEnum, BulletAssetData> BulletAssets => _Instance._BulletAssets;

        #endregion

        #region 生命周期

        private void Awake()
        {
            if (_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (_BulletAssets == null || _BulletObject == null)
            {
                Debug.LogError("Bullet Manager unset, plz chack your Kellner");
            }
        }

        #endregion

        #region 方法实现与API

        // NOTE: 部分方法职责徐进一步分化

        private BulletInitData _BulletInitFactory(PlaneIdentity identity, AmmoEnum type)
        {
            BulletAssetData _cache = _BulletAssets[type];
            return new BulletInitData(identity, type, _cache.Speed, _cache.LastTime);
        }

        internal bool Shoot(PlanePropertiesHandler plane, Vector3 location, Quaternion direction)
        {
            var _ammo = plane.LoadedAmmo;
            var _identity = plane.Identity;
            //float _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            BulletInitData _cache = _BulletInitFactory(_identity, _ammo);

            GameObject _1;
            switch(_ammo)
            {
                case AmmoEnum.NORMAL:
                    _1 = Instantiate(_BulletObject, location, direction);
                    //_1.SetActive(false);
                    // 提供子弹运动与碰撞所需参数
                    _1.GetComponent<BulletController>().Init(_cache);
                    // 设置(覆盖Default)贴图
                    _1.GetComponent<SpriteRenderer>().sprite = _BulletSprites[_identity];
                    //_1.SetActive(true);
                    break;
                case AmmoEnum.SNIPER:
                    _1 = Instantiate(_BulletObject, location, direction);
                    break;
                case AmmoEnum.TRACE:
                    _1 = Instantiate(_BulletObject, location, direction);
                    break;
                case AmmoEnum.TRIANT:
                    _1 = Instantiate(_BulletObject, location, direction);
                    break;
                case AmmoEnum:
                    _1 = null;
                    break;
            };
            return true;
        }

        // 打算弃用
        internal void Dismiss(GameObject bulletObject) =>
            Destroy(bulletObject);


        internal void Hit(GameObject bulletObject, PlanePropertiesHandler plane)
        {
            if (!bulletObject.GetComponent<BulletController>())
            {
                Debug.LogError("!!!No bulletController");
                return;
            }
            if (bulletObject.GetComponent<BulletController>().Identity == plane.Identity)
                return;

            AmmoEnum type = bulletObject.GetComponent<BulletController>().AmmoType;
            Destroy(bulletObject);

            plane.TryCauseDamage(_BulletAssets[type].Damage);
        }

        #endregion
    }
}

