using UnityEngine;

using FlightFight.GamePlay.Movers;
using FlightFight.GamePlay.Controllers.Base;

namespace FlightFight.GamePlay.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class BotController: Controller
    {
        [SerializeField] private float _AccelerationFactor = 0.1f;

        private float _Timer = 0f;

        void Start()
        {
            _SelfRigidBody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _Timer += Time.fixedDeltaTime;
            if (_GlobalGameManager)
            {
                _FaceTargetDir = - _GlobalGameManager.GetFaceVector();
                _FaceLock();
            }
            else
            {
                Debug.LogError("No GlobalGameManager is set");
            }

            if (_Timer > 0.5)
            {
                EnemyMove.BotMove(_SelfRigidBody2D, _MoveSpeed, _AccelerationFactor);
                _Timer -= 0.5f;
            }

        }
    }

}