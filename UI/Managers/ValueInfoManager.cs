using System.Collections.Generic;
using UnityEngine;

using FlightFight.Shared.Data;

using TMPro;
using Audune.Utils.Dictionary;
using System;

namespace FlightFight.UI.Managers
{
    public class ValueInfoManager : MonoBehaviour
    {
        #region 私有与序列字段

        [Header("参数面板")]
        [Obsolete(" 请使用 _InfoTexts 替代")]
        [SerializeField] private SerializableDictionary<ValueInfoData.Sid, TMP_Text> _SidTexts = new();

        [Header("参数设置")]
        [SerializeField] private float _UpdateFPS;

        private readonly Dictionary<ValueInfoData, TMP_Text> _InfoTexts = new();

        private readonly Dictionary<ValueInfoData, string> _TextPatterns = new()
        {
            {ValueInfoData.SELF_HEALTH, "Player Health:" },

            {ValueInfoData.SELF_ENERGY, "Player Energy:" },

            {ValueInfoData.ENEMY_HEALTH, "Enemy Health:" },

            {ValueInfoData.ENEMY_ENERGY, "Enemy Energy:" }
        };

        private Dictionary<ValueInfoData, float> _RawInfo = new()
        {
            {ValueInfoData.SELF_HEALTH, 0.0f },
            {ValueInfoData.SELF_ENERGY, 0.0f },
            {ValueInfoData.ENEMY_HEALTH, 0.0f },
            {ValueInfoData.ENEMY_ENERGY, 0.0f }
        };

        private bool _IsInit = false;

        #endregion

        #region 公开字段

        public bool IsInit => _IsInit;

        #endregion

        #region 方法实现

        // NOTE: 部分方法徐进一步优化和修改，废除冗余方法

        private void _SafeDictionalize()
        {
#pragma warning disable CS0618
            foreach (var i in _SidTexts.Keys)
            {
                _InfoTexts.Add(ValueInfoData.Standard[i], _SidTexts[i]);
            }
            _SidTexts = null;
#pragma warning restore CS0618

        }

        private void _UpdateByType(ValueInfoData type)
        {
            _InfoTexts[type].text = _TextPatterns[type] + $" {_RawInfo[type]:F1}";
        }

        private void _UpdateAll()
        {
            _UpdateByType(ValueInfoData.SELF_HEALTH);
            _UpdateByType(ValueInfoData.SELF_ENERGY);
            _UpdateByType(ValueInfoData.ENEMY_HEALTH);
            _UpdateByType(ValueInfoData.ENEMY_ENERGY);
        }

        private void _SetAll(float selfHealth, float selfEnergy, float enemyHealth, float enemyEnergy)
        {
            SetInfoTo(ValueInfoData.SELF_HEALTH, selfHealth);
            SetInfoTo(ValueInfoData.SELF_ENERGY, selfEnergy);
            SetInfoTo(ValueInfoData.ENEMY_HEALTH, enemyHealth);
            SetInfoTo(ValueInfoData.ENEMY_ENERGY, enemyEnergy);
            _UpdateAll();
        }

        #endregion

        #region 对外API

        public void Init(float selfHealth = 0.0f, float selfEnergy = 0.0f, float enemyHealth = 0.0f, float enemyEnergy = 0.0f)
        {
            if (!_IsInit)
            {
                _SafeDictionalize();
                _SetAll(selfHealth, selfEnergy, enemyHealth, enemyEnergy);
                _IsInit = true;
            }
            else
            {
                Debug.LogWarning("Repeated init, invalid oeration.");
            }
        }

        public void SetInfoTo(ValueInfoData infoType, float value)
        {
            _RawInfo[infoType] = value;
            _UpdateByType(infoType);
        }

        /*
        public void SetInfoBy(ValueInfoData infoType, float deltaValue)
        {
            _RawInfo[infoType] += deltaValue;
            _UpdateByType(infoType);
        }
        */

        #endregion
    }
}
