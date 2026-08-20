using System;
using System.Collections.Generic;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.Shared.Data
{
    [Serializable]
    public struct ValueInfoData
    {
        // 序列化需求
        public enum Sid
        {
            SELF_HEALTH,
            SELF_ENERGY,
            ENEMY_HEALTH,
            ENEMY_ENERGY
        }

        private static readonly Dictionary<Sid, ValueInfoData> _Standard = new()
        {
            {Sid.SELF_HEALTH, new(PlaneIdentity.SELF, ValueInfoEnum.HEALTH) },
            {Sid.SELF_ENERGY, new(PlaneIdentity.SELF, ValueInfoEnum.ENERGY) },
            {Sid.ENEMY_HEALTH, new(PlaneIdentity.ENEMY, ValueInfoEnum.HEALTH) },
            {Sid.ENEMY_ENERGY, new(PlaneIdentity.ENEMY, ValueInfoEnum.ENERGY) }
        };

        public static IReadOnlyDictionary<Sid, ValueInfoData> Standard => _Standard;

        public static ValueInfoData SELF_HEALTH => _Standard[Sid.SELF_HEALTH];

        public static ValueInfoData SELF_ENERGY => _Standard[Sid.SELF_ENERGY] ;

        public static ValueInfoData ENEMY_HEALTH => _Standard[Sid.ENEMY_HEALTH] ;

        public static ValueInfoData ENEMY_ENERGY => _Standard[Sid.ENEMY_ENERGY] ;

        public PlaneIdentity Identity;

        public ValueInfoEnum Type;

        public ValueInfoData(PlaneIdentity identity, ValueInfoEnum type)
        {
            Identity = identity;
            Type = type;
        }
        public override bool Equals(object obj)
        {
            if (obj is ValueInfoData other)
                return Identity == other.Identity && Type == other.Type;
            return false;
        }

        public override int GetHashCode()
        {
            return Identity.GetHashCode() * 31 + Type.GetHashCode();
        }
    }
}
