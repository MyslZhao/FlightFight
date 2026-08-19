using System.Collections.Generic;
using UnityEngine;

using FlightFight.Shared.Data;

using TMPro;
using Audune.Utils.Dictionary;
using System;

namespace FlightFight.UI.Managers
{
    public class InfoPanelManager : MonoBehaviour
    {
        #region 私有与序列字段

        [Header("参数面板")]
        [Obsolete(" 请使用 _InfoTexts 替代")]
        [SerializeField] private SerializableDictionary<InfoData.Sid, TMP_Text> _SidTexts = new();

        [Header("参数设置")]
        [SerializeField] private float _UpdateFPS;

        private Dictionary<InfoData, TMP_Text> _InfoTexts = new();

        private readonly Dictionary<InfoData, string> _TextPatterns = new()
        {
            {InfoData.SELF_HEALTH, "Player Health:" },

            {InfoData.SELF_ENERGY, "Player Energy:" },

            {InfoData.ENEMY_HEALTH, "Enemy Health:" },

            {InfoData.ENEMY_ENERGY, "Enemy Energy:" }
        };

        private Dictionary<InfoData, float> _RawInfo = new()
        {
            {InfoData.SELF_HEALTH, 0.0f },
            {InfoData.SELF_ENERGY, 0.0f },
            {InfoData.ENEMY_HEALTH, 0.0f },
            {InfoData.ENEMY_ENERGY, 0.0f }
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
#pragma warning disable CS0612
#pragma warning disable CS0618
            foreach (var i in _SidTexts.Keys)
            {
                _InfoTexts.Add(InfoData.Standard[i], _SidTexts[i]);
            }
            _SidTexts = null;
#pragma warning restore CS0612
#pragma warning restore CS0618

        }

        private void _UpdateByType(InfoData type)
        {
            _InfoTexts[type].text = _TextPatterns[type] + $" {_RawInfo[type]:F1}";
        }

        private void _UpdateAll()
        {
            _UpdateByType(InfoData.SELF_HEALTH);
            _UpdateByType(InfoData.SELF_ENERGY);
            _UpdateByType(InfoData.ENEMY_HEALTH);
            _UpdateByType(InfoData.ENEMY_ENERGY);
        }

        private void _SetAll(float selfHealth, float selfEnergy, float enemyHealth, float enemyEnergy)
        {
            SetInfoTo(InfoData.SELF_HEALTH, selfHealth);
            SetInfoTo(InfoData.SELF_ENERGY, selfEnergy);
            SetInfoTo(InfoData.ENEMY_HEALTH, enemyHealth);
            SetInfoTo(InfoData.ENEMY_ENERGY, enemyEnergy);
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

        public void SetInfoTo(InfoData infoType, float value)
        {
            _RawInfo[infoType] = value;
            _UpdateByType(infoType);
        }

        public void SetInfoBy(InfoData infoType, float deltaValue)
        {
            _RawInfo[infoType] += deltaValue;
            _UpdateByType(infoType);
        }

        #endregion
    }
}
