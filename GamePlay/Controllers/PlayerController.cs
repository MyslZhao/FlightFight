using UnityEngine;
using UnityEngine.InputSystem;

using FlightFight.GamePlay.Controllers.Base;

namespace FlightFight.GamePlay.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class PlayerController: Controller
    {
        private Vector2 _moveInput;

        void Start()
        {
            _SelfRigidBody2D = GetComponent<Rigidbody2D>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = Vector2.zero;
            _moveInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            if (_GlobalGameManager)
            {
                _FaceTargetDir = _GlobalGameManager.GetFaceVector();
                _FaceLock();
            }
            else
            {
                Debug.LogError("No GlobalGameManager is set");
            }

            if (Vector2.zero != _moveInput)
            {
                _SelfRigidBody2D.linearVelocity = _moveInput * _MoveSpeed;
            }
            else
            {
                _SelfRigidBody2D.linearVelocity = Vector2.zero;
            }
        }
    }

}
