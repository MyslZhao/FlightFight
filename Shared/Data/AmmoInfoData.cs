using System;
using System.Collections.Generic;
using System.Text;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.Shared.Data
{
    // NOTE: 初始化数据
    // 可用性还待观察

    public struct AmmoInfoData
    {
        public readonly PlaneIdentity Identity;

        public readonly Sprite LoadedSprite;

        public AmmoInfoData(PlaneIdentity identity, Sprite sprite)
        {
            Identity = identity;
            LoadedSprite = sprite;
        }
    }
}
