using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test-Only
// [Obsolete]
namespace FlightFight.GamePlay.Movers
{
    internal static class EnemyMove
    {
        // BUG!!
        private static readonly Action<Rigidbody2D, Vector2, float, float>[] BotMoveLogic =
        {
            (_rb, _moveDir, _moveSpeed, _aFactor) =>
            {
                Vector2 _cache =  (_moveDir * _aFactor).normalized * _moveSpeed;
                _rb.linearVelocity = _cache;
            }
        };

        // BUG!!
        public static void BotMove(Rigidbody2D BotRigidbody, float BotSpeed, float AccelerationFactor)
        {
            System.Random rand = new System.Random();

            BotMoveLogic[0](
                BotRigidbody,
                new Vector2((float) rand.NextDouble() - 0.5f, (float) rand.NextDouble() - 0.5f),
                BotSpeed,
                AccelerationFactor
                );

        }

        public static void OtherMove()
        {

        }
    }
}



