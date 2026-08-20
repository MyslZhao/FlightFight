using UnityEngine;

using FlightFight.GamePlay.Movers;
using FlightFight.GamePlay.Managers;
using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Handlers;

namespace FlightFight.GamePlay.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlanePropertiesHandler))]
    internal class BotController: Controller
    {
        [SerializeField] private float _AccelerationFactor = 0.1f;

        private float _Timer = 0f;

        void Start()
        {
            _SelfRigidBody2D = GetComponent<Rigidbody2D>();
            _planeProperties = GetComponent<PlanePropertiesHandler>();
        }

        void FixedUpdate()
        {
            _Timer += Time.fixedDeltaTime;

            _FaceTargetDir = -GlobalGameManager.GetFaceVector();
            _FaceLock();

            if (_Timer > 0.5)
            {
                EnemyMove.BotMove(_SelfRigidBody2D, _MoveSpeed, _AccelerationFactor);
                _Timer -= 0.5f;
            }

        }
    }

}