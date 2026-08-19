using System;
using System.Collections.Generic;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.Shared.Data
{
    [Serializable]
    public struct InfoData
    {
        // 序列化需求
        public enum Sid
        {
            SELF_HEALTH,
            SELF_ENERGY,
            ENEMY_HEALTH,
            ENEMY_ENERGY
        }

        private static readonly Dictionary<Sid, InfoData> _Standard = new()
        {
            {Sid.SELF_HEALTH, new(PlaneIdentity.SELF, InfoEnum.HEALTH) },
            {Sid.SELF_ENERGY, new(PlaneIdentity.SELF, InfoEnum.ENERGY) },
            {Sid.ENEMY_HEALTH, new(PlaneIdentity.ENEMY, InfoEnum.HEALTH) },
            {Sid.ENEMY_ENERGY, new(PlaneIdentity.ENEMY, InfoEnum.ENERGY) }
        };

        public static IReadOnlyDictionary<Sid, InfoData> Standard => _Standard;

        public static InfoData SELF_HEALTH => _Standard[Sid.SELF_HEALTH];

        public static InfoData SELF_ENERGY => _Standard[Sid.SELF_ENERGY] ;

        public static InfoData ENEMY_HEALTH => _Standard[Sid.ENEMY_HEALTH] ;

        public static InfoData ENEMY_ENERGY => _Standard[Sid.ENEMY_ENERGY] ;

        public PlaneIdentity Identity;

        public InfoEnum Type;

        public InfoData(PlaneIdentity identity, InfoEnum type)
        {
            Identity = identity;
            Type = type;
        }
        public override bool Equals(object obj)
        {
            if (obj is InfoData other)
                return Identity == other.Identity && Type == other.Type;
            return false;
        }

        public override int GetHashCode()
        {
            return Identity.GetHashCode() * 31 + Type.GetHashCode();
        }
    }
}
