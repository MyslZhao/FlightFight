using FlightFight.GamePlay.Handlers;
using FlightFight.GamePlay.Managers;
using FlightFight.Shared.Data;
using FlightFight.Shared.Enums;
using UnityEngine;

namespace FlightFight.GamePlay.Controllers.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Transform))]
    public class BulletController: MonoBehaviour
    {
        #region 私有字段

        private PlaneIdentity _Identity;

        private AmmoEnum _Type;

        private Rigidbody2D _Rigidbody;

        private Transform _Transform;

        private float _LifeTime;

        private float _TraceFactor;

        private float _CurrentTime = 0.0f;

        #endregion

        #region 公开字段

        public PlaneIdentity Identity => _Identity;

        public AmmoEnum AmmoType => _Type;

        #endregion

        #region 生命周期

        private void Awake()
        {
            _Rigidbody = GetComponent<Rigidbody2D>();
            _Transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            var _1 = _Rigidbody.linearVelocity.magnitude;
            _Rigidbody.linearVelocity = 
                (_Rigidbody.linearVelocity + _TraceFactor * GlobalGameManager.GetFaceTo(_Identity, _Transform.position))
                .normalized * _1;

            _CurrentTime += Time.fixedDeltaTime;
            if (_CurrentTime > _LifeTime)
            {
                GlobalGameManager.OnMiss(gameObject);
                // Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
                return;

            if (other.CompareTag("Wall"))
            {
                GlobalGameManager.OnMiss(gameObject);
                // Destroy(gameObject);

                return;
            }

            var _1 = other.GetComponent<PlanePropertiesHandler>().Identity;

            if (_1 != _Identity)
            {
                GlobalGameManager.OnHit(_1, _Type);
                Destroy(gameObject);
            }
        }

        #endregion

        #region 外部API

        internal void Init(BulletInitData bulletData)
        {
            _Identity = bulletData.Identity;
            _Type = bulletData.Type;
            _LifeTime = bulletData.LastTime;
            _TraceFactor = bulletData.TraceFactor;


            //if (!_Rigidbody)
            //{
            //    Debug.LogError("???No Rigidbody?!");
            //    return;
            //}

            _Rigidbody.linearVelocity = transform.up * bulletData.Speed;

            GetComponent<BoxCollider2D>().enabled = true;
        }

        #endregion
    }
}
