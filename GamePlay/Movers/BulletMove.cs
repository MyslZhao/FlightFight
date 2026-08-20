using System;
using System.Collections.Generic;
using FlightFight.Shared.Data;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.GamePlay.Movers
{
    internal static class BulletMove
    {
        private static readonly Dictionary<AmmoEnum, Action> _MovesLists = new()
        {
            {AmmoEnum.NORMAL, () => {
                return;
            } },

            {AmmoEnum.SNIPER, () => { 
            
            } },

            {AmmoEnum.TRACE, () => { 
            
            } },

            {AmmoEnum.TRIANT, () => {
            
            } }
        };

        private static readonly Dictionary<AmmoEnum, Action<Rigidbody2D, Transform, float>> _MotionInits = new()
        {
            {AmmoEnum.NORMAL, (rigidbody2D, transform, initSpeed) => {
                rigidbody2D.linearVelocity = transform.up * initSpeed;
            } },

            {AmmoEnum.SNIPER, (rigidbody2D, transform, initSpeed) => {

            } },

            {AmmoEnum.TRACE, (rigidbody2D, transform, initSpeed) => {

            } },

            {AmmoEnum.TRIANT, (rigidbody2D, transform, initSpeed) => {

            } }
        };

        internal static IReadOnlyDictionary<AmmoEnum, Action> MoveLists => _MovesLists;

        internal static IReadOnlyDictionary<AmmoEnum, Action<Rigidbody2D, Transform, float>> MotionInits => _MotionInits;
    }
}
