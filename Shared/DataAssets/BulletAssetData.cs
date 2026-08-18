using UnityEngine;

using FlightFight.Shared.Enums;

using Audune.Utils.Dictionary;
using System;

namespace FlightFight.Shared.DataAssets
{
    [CreateAssetMenu(fileName = "BulletAssetData", menuName = "Scriptable Objects/BulletAssetData")]
    public class BulletAssetData: ScriptableObject
    {
        [Header("基本属性")]

        [SerializeField] private AmmoEnum _Name;

        [SerializeField] private float _Damage;

        [SerializeField] private float _Speed;

        [SerializeField] private float _Energy;

        [SerializeField] private int _Storage;

        [SerializeField] private float _LastTime;

        [Header("样式")]

        [SerializeField] private SerializableDictionary<PlaneIdentity, Sprite> _IconSprites;

        public AmmoEnum Name => _Name;

        public float Damage => _Damage;

        public float Speed => _Speed;

        public float Energy => _Energy;

        public int Storage => _Storage;

        public float LastTime => _LastTime;

        public SerializableDictionary<PlaneIdentity, Sprite> IconSprites => _IconSprites;
    }

}

