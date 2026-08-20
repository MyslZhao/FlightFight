using UnityEngine;

using FlightFight.GamePlay.Managers;
using FlightFight.GamePlay.Handlers;

namespace FlightFight.GamePlay.Controllers.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller: MonoBehaviour
    {
        [Header("基础属性")]
        [SerializeField] protected float _MoveSpeed = 5.0f;

        protected Rigidbody2D _SelfRigidBody2D;

        protected PlanePropertiesHandler _planeProperties;

        protected Vector2 _FaceTargetDir;

        protected void _FaceLock() =>
            _SelfRigidBody2D.SetRotation(Mathf.Atan2(_FaceTargetDir.y, _FaceTargetDir.x) * Mathf.Rad2Deg - 90);

    }

}
