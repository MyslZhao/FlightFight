using System;
using System.Collections.Generic;
using System.Text;
using FlightFight.Shared.Enums;

namespace FlightFight.Shared.Data
{
    public readonly struct InfoData
    {
        public static InfoData SELF_HEALTH = new(PlaneIdentity.SELF, InfoEnum.HEALTH);

        public static InfoData SELF_ENERGY = new(PlaneIdentity.SELF, InfoEnum.ENERGY);

        public static InfoData ENEMY_HEALTH = new(PlaneIdentity.ENEMY, InfoEnum.HEALTH);

        public static InfoData ENEMY_ENERGY = new(PlaneIdentity.ENEMY, InfoEnum.ENERGY);
        public PlaneIdentity Identity { get; }
        public InfoEnum Type { get; }

        public InfoData(PlaneIdentity identity, InfoEnum type)
        {
            Identity = identity;
            Type = type;
        }
    }
}
