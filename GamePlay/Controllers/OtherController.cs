using UnityEngine;

using FlightFight.GamePlay.Controllers.Base;

namespace FlightFight.GamePlay.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class OtherController: Controller
    {

        void Start()
        {

        }

        void FixedUpdate()
        {
            // EnemyMove.OtherMove();
        }
    }
}
