using System;
using FlightFight.Shared.Enums;
using UnityEngine;
using System.Collections.Generic;

namespace FlightFight.GamePlay.Managers
{
    public class DataTransManager: MonoBehaviour
    {
        #region 私有字段与序列字段

        [Header("Test模式")]
        [SerializeField]private bool EnableTest = false;

        [Header("玩家配置")]
        [Obsolete]
        [SerializeField]private AmmoEnum[] _SelfAmmo = new AmmoEnum[5];

        [Obsolete]
        [SerializeField]private AmmoEnum[] _EnemyAmmo = new AmmoEnum[5];

        private static DataTransManager _Instance;

        private bool _IsReady = false;

        private readonly Dictionary<PlaneIdentity, AmmoEnum[]> _AmmoLists = new();

        private readonly Dictionary<PlaneIdentity, int> _AmmoNumbers = new();

        #endregion

        #region 公开字段

        internal static bool IsReady => _Instance._IsReady;

        internal static AmmoEnum[] SelfAmmo => _Instance._AmmoLists[PlaneIdentity.SELF];

        internal static AmmoEnum[] EnemyAmmo => _Instance._AmmoLists[PlaneIdentity.ENEMY];

        #endregion

        #region 生命周期

        void Awake()
        {
            if (_Instance == null)
            {
                _Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (EnableTest)
            {
                _SafeDictionalize();
                foreach (var i in _AmmoNumbers.Keys)
                {
                    _AmmoNumbers[i] = 5;
                }

                _CheckReady();
            }
            else
            {
                foreach(var i in _AmmoLists.Keys)
                {
                    _AmmoNumbers.Add(i, 0);
                }
            }
        }

        #endregion

        #region 方法实现

        private void _SafeDictionalize()
        {
#pragma warning disable CS0612
            Debug.Assert(_SelfAmmo != null, "Your SelfAmmo is None!");
            Debug.Assert(_EnemyAmmo != null, "Your EnemyAmmo is None!");

            _AmmoLists.Add(PlaneIdentity.SELF, _SelfAmmo);
            _AmmoLists.Add(PlaneIdentity.ENEMY, _EnemyAmmo);

            _SelfAmmo = null;
            _EnemyAmmo = null;
#pragma warning restore CS0612
        }

        private void _CheckReady()
        {
            foreach (var i in _AmmoNumbers.Keys)
            {
                if (_AmmoNumbers[i] != 5)
                {
                    return;
                }
            }
            _IsReady = true;
        }

        private void _DeployAmmo(PlaneIdentity identity, AmmoEnum ammo)
        {
            var _1 = _AmmoLists[identity];
            _1[_AmmoNumbers[identity]] = ammo;
            _AmmoNumbers[identity] += 1;
        }

        #endregion

        #region 对外API

        public void DeployAmmo(PlaneIdentity identity, AmmoEnum ammo)
        {
            _DeployAmmo(identity, ammo);

            _CheckReady();
        }

        #endregion

    }

}
