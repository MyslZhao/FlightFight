using System;
using FlightFight.GamePlay.Handlers;
using UnityEngine;

// Test-Only
// [Obsolete]
namespace FlightFight.GamePlay.Movers
{
    internal static class EnemyMove
    {
        static private readonly System.Random _Types = new();

        static private readonly System.Random _Odds = new();

        static private readonly Action<Rigidbody2D, PlanePropertiesHandler, Vector2, float>[] BotMoveLogic =
        {
            (_rb, properties, _moveDir, _moveSpeed) =>
            {
                Vector2 _cache =  _moveDir.normalized * _moveSpeed;
                _rb.linearVelocity = _cache;
            },
            (_rb, properties, _moveDir, _moveSpeed) =>
            {
                properties.Shoot();
            }
        };

        static internal void BotMove(Rigidbody2D rigidbody, PlanePropertiesHandler planeProperties, float speed) =>
            BotMoveLogic[_Types.Next(0, 2)](
                rigidbody,
                planeProperties,
                new Vector2((float) _Odds.NextDouble() - 0.5f, (float) _Odds.NextDouble() - 0.5f),
                speed
                );

        static internal void OtherMove()
        {

        }
    }
}



