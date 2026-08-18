using UnityEngine;
using UnityEngine.InputSystem;

using FlightFight.GamePlay.Controllers.Base;
using FlightFight.GamePlay.Managers;
using FlightFight.GamePlay.Ammo;
using FlightFight.Shared.Enums;

namespace FlightFight.GamePlay.Handlers
{
    [RequireComponent(typeof(Controller))]
    public class PlanePropertiesHandler: MonoBehaviour
    {
        [Header("基本信息")]
        [SerializeField] private PlaneIdentity _Identity;

        [SerializeField] private float _MaxHealth = 100;

        [SerializeField] private float _MaxEnergy = 30.0f;

        [SerializeField] private float _RecoverDeltaTime = 0.1f;

        [SerializeField] private float _RecoverFactor = 1.0f;

        [SerializeField] private GlobalGameManager _GlobalGameManager;

        private float _CurrentHealth;

        private float _CurrentEnergy;

        private AmmoGroup _BulletGroup;

        private float _RecoverCurrentTime = 0.0f;
        public PlaneIdentity Identity => _Identity;

        internal float MaxHealth => _MaxHealth;

        internal float MaxEnergy => _MaxEnergy;

        internal float Health => _CurrentHealth;

        internal float Energy => _CurrentEnergy;

        internal AmmoEnum LoadedAmmo => _BulletGroup.LoadedAmmo;

        private void Awake()
        {
            _CurrentHealth = _MaxHealth;
            _CurrentEnergy = _MaxEnergy;

            switch (Identity)
            {
                case PlaneIdentity.SELF:
                    _BulletGroup = new AmmoGroup(DataTransManager.SelfAmmo);
                    break;
                case PlaneIdentity.ENEMY:
                    _BulletGroup = new AmmoGroup(DataTransManager.EnemyAmmo);
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (_CurrentEnergy < _MaxEnergy)
            {
                _RecoverCurrentTime += Time.fixedDeltaTime;
                if (_RecoverCurrentTime >= _RecoverDeltaTime)
                {
                    _CurrentEnergy += _RecoverFactor * _RecoverDeltaTime;
                    _RecoverCurrentTime -= _RecoverDeltaTime;
                }
            }
            else
            {
                _RecoverCurrentTime = 0;
            }
        }

        private bool _IsShootable(AmmoEnum type)
        {
            float _energyCost = BulletManager.BulletAssets[type].Energy;
            return _energyCost <= _CurrentEnergy;
        }

        internal void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var _1 = _BulletGroup.LoadedAmmo;
                if (_IsShootable(_1))
                {
                    var _cache = transform.position + transform.up * 0.2f;
                    _GlobalGameManager.OnShoot(_Identity);
                }
            }
        }
        
        // 对外API

        internal void CauseDamage(float damage)
        {
            _CurrentHealth -= damage;
            if (_CurrentHealth < 0)
                _CurrentHealth = 0;
        }

        private void _ConsumeEnergy()
        {
            _CurrentEnergy -= BulletManager.BulletAssets[LoadedAmmo].Energy;
            if (_CurrentEnergy < 0)
                _CurrentEnergy = 0;
        }

        private void _ConsumeAmmo()
        {

        }
    }

}

