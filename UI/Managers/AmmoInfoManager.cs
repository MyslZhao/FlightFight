using UnityEngine;

using Audune.Utils.Dictionary;
using FlightFight.Shared.Enums;
using UnityEngine.UI;
using FlightFight.Shared.Data;

namespace FlightFight.UI.Manager
{
    public class AmmoInfoManager : MonoBehaviour
    {
        #region 私有与序列字段

        [SerializeField] private SerializableDictionary<PlaneIdentity, Image> _AmmoSprites = new();

        #endregion

        #region 方法实现

        private void _SetAmmoTo(PlaneIdentity identity, Sprite sprite)
        {
            _AmmoSprites[identity].sprite = sprite;
        }

        #endregion

        #region 对外API

        public void SetAmmoTo(PlaneIdentity identity, Sprite sprite) =>
            _SetAmmoTo(identity, sprite);

        public void Init(AmmoInfoData selfData, AmmoInfoData enemyData)
        {
            SetAmmoTo(selfData.Identity, selfData.LoadedSprite);
            SetAmmoTo(enemyData.Identity, enemyData.LoadedSprite);
        }

        #endregion
    }
}
