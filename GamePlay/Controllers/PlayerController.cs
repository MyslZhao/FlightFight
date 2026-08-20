using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Handlers;
using FlightFight.GamePlay.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlightFight.GamePlay.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlanePropertiesHandler))]
    internal class PlayerController: Controller
    {
        private Vector2 _moveInput;

        void Start()
        {
            _SelfRigidBody2D = GetComponent<Rigidbody2D>();
            _planeProperties = GetComponent<PlanePropertiesHandler>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = Vector2.zero;
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _planeProperties.Shoot();
            }
        }

        private void FixedUpdate()
        {
            _FaceTargetDir = GlobalGameManager.GetFaceTo(_planeProperties.Identity, transform.position);
            _FaceLock();


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
