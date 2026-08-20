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
        #region 私有与序列字段

        [Header("基本信息")]
        [SerializeField] private PlaneIdentity _Identity;

        [SerializeField] private float _MaxHealth = 100;

        [SerializeField] private float _MaxEnergy = 30.0f;

        [SerializeField] private float _RecoverDeltaTime = 0.1f;

        [SerializeField] private float _RecoverFactor = 1.0f;

        private float _CurrentHealth;

        private float _CurrentEnergy;

        private AmmoGroup _BulletGroup;

        private float _RecoverCurrentTime = 0.0f;

        #endregion

        #region 公开字段

        internal PlaneIdentity Identity => _Identity;

        internal float MaxHealth => _MaxHealth;

        internal float MaxEnergy => _MaxEnergy;

        internal float Health => _CurrentHealth;

        internal float Energy => _CurrentEnergy;

        internal AmmoEnum LoadedAmmo => _BulletGroup.LoadedAmmo;

        #endregion

        #region 生命周期

        private void Start()
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
                    GlobalGameManager.UpdateEnergy(_Identity, _CurrentEnergy);
                    _RecoverCurrentTime -= _RecoverDeltaTime;
                }
            }
            else
            {
                _RecoverCurrentTime = 0;
            }

            
        }

        internal void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var _1 = _BulletGroup.LoadedAmmo;
                if (_IsShootable(_1))
                {
                    var _cache = transform.position + transform.up * 0.2f;
                    GlobalGameManager.OnShoot(_Identity);
                }
            }
        }

        #endregion

        #region 方法实现

        private bool _IsShootable(AmmoEnum type)
        {
            float _energyCost = BulletManager.BulletAssets[type].Energy;
            return _energyCost <= _CurrentEnergy;
        }

        private void _TryConsumeEnergy()
        {
            var _1 = BulletManager.BulletAssets[LoadedAmmo].Energy;
            Debug.Assert(_CurrentEnergy >= _1, "Your 'TryConsumeEnergy' makes the energy the negative ones.");
            _CurrentEnergy -= _1;
        }

        private void _TryConsumeAmmo() =>
            _BulletGroup.TryConsume();


        private void _TryCauseDamage(float damage)
        {
            _CurrentHealth -= damage;
            Debug.Assert(_CurrentHealth >= 0, "");
            if (_CurrentHealth < 0)
                _CurrentHealth = 0;
        }

        #endregion

        #region 对外API

        internal void TryShoot()
        {
            _TryConsumeEnergy();
            _TryConsumeAmmo();
        }

        internal void TryCauseDamage(float damage) =>
            _TryCauseDamage(damage);

        #endregion
    }

}
