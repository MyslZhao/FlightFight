using UnityEngine;

using FlightFight.GamePlay.Managers;

namespace FlightFight.GamePlay.Controllers.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller: MonoBehaviour
    {
        [Header("基础属性")]
        [SerializeField] protected float _MoveSpeed = 5.0f;

        protected GlobalGameManager _GlobalGameManager;

        protected Rigidbody2D _SelfRigidBody2D;

        protected Vector2 _FaceTargetDir;

        public void SetGameManager(GlobalGameManager globalGameManager) =>
            this._GlobalGameManager = globalGameManager;


        protected void _FaceLock() =>
            _SelfRigidBody2D.SetRotation(Mathf.Atan2(_FaceTargetDir.y, _FaceTargetDir.x) * Mathf.Rad2Deg - 90);

    }

}
