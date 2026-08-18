using UnityEngine;

using FlightFight.GamePlay.Managers;
using FlightFight.Shared.Enums;
using FlightFight.GamePlay.Handlers;
using FlightFight.Shared.Data;

namespace FlightFight.GamePlay.Controllers.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletController: MonoBehaviour
    {
        private PlaneIdentity _Identity;

        private AmmoEnum _Type;

        private Rigidbody2D _Rigidbody;

        private BulletManager _Manager;

        private float _LifeTime;

        private float _CurrentTime = 0.0f;

        public PlaneIdentity Identity => _Identity;

        public AmmoEnum AmmoType => _Type;

        private void Awake()
        {
            _Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _CurrentTime += Time.fixedDeltaTime;
            if (_CurrentTime > _LifeTime)
            {
                _Manager.Dismiss(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Wall") || other.CompareTag("Bullet"))
            {
                _Manager.Dismiss(gameObject);
                return;
            }

            _Manager.Hit(gameObject, other.GetComponent<PlanePropertiesHandler>());
        }

        // 外部API

        internal void Init(BulletManager manager, BulletInitData bulletData)
        {
            _Identity = bulletData.Identity;
            _Type = bulletData.Type;
            _LifeTime = bulletData.LastTime;
            _Manager = manager;

            //if (!_Rigidbody)
            //{
            //    Debug.LogError("???No Rigidbody?!");
            //    return;
            //}

            _Rigidbody.linearVelocity = transform.up * bulletData.Speed;
        }
    }
}
