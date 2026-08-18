using UnityEngine;

namespace FlightFight.Shared.DataAssets
{
    [CreateAssetMenu(fileName = "ItemAssetData", menuName = "Scriptable Objects/ItemAssetData")]
    public class ItemAssetData: ScriptableObject
    {
        [Header("基本属性")]
        public string itemName;

        public Sprite itemSprite;
    }

}