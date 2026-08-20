using System;
using System.Collections.Generic;
using FlightFight.Shared.Data;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.GamePlay.Movers
{
    internal static class BulletMove
    {
        private static readonly Dictionary<AmmoEnum, Action<Rigidbody2D, Vector2>> _MovesLists = new()
        {
            {AmmoEnum.NORMAL, (rb, dir) => {
                return;
            } },

            {AmmoEnum.SNIPER, (rb, dir) => {
                return;
            } },

            {AmmoEnum.TRACE, (rb, dir) => {
                var _1 = rb.linearVelocity.magnitude;
                rb.linearVelocity = (rb.linearVelocity + dir).normalized * _1;
            } },

            {AmmoEnum.TRIANT, (rb, dir) => {
                return;
            } }
        };

        //NOTE: 初始化使用
        //观察后续是否还有留存必要

        /*
        private static readonly Dictionary<AmmoEnum, Action<Rigidbody2D, Transform, float>> _MotionInits = new()
        {
            {AmmoEnum.NORMAL, (rigidbody2D, transform, initSpeed) => {
                rigidbody2D.linearVelocity = transform.up * initSpeed;
            } },

            {AmmoEnum.SNIPER, (rigidbody2D, transform, initSpeed) => {
                rigidbody2D.linearVelocity = transform.up * initSpeed;
            } },

            {AmmoEnum.TRACE, (rigidbody2D, transform, initSpeed) => {
                rigidbody2D.linearVelocity = transform.up * initSpeed;
            } },

            {AmmoEnum.TRIANT, (rigidbody2D, transform, initSpeed) => {
                rigidbody2D.linearVelocity = transform.up * initSpeed;
            } }
        };
        */

        internal static IReadOnlyDictionary<AmmoEnum, Action<Rigidbody2D, Vector2>> MoveLists => _MovesLists;

        //internal static IReadOnlyDictionary<AmmoEnum, Action<Rigidbody2D, Transform, float>> MotionInits => _MotionInits;
    }
}
