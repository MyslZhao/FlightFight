using System.Collections.Generic;
using UnityEngine;

using FlightFight.Shared.Enums;
using FlightFight.Shared.Data;

using TMPro;
using Audune.Utils.Dictionary;

namespace FlightFight.UI.Managers
{
    public class InfoPanelManager : MonoBehaviour
    {
        [Header("参数面板")]

        [SerializeField] private SerializableDictionary<InfoData, TMP_Text> _InfoTexts;

        [Header("参数设置")]
        [SerializeField] private float _UpdateFPS;

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

        public bool IsInit => _IsInit;

        private void _UpdateByType(InfoData type)
        {
            _InfoTexts[type].text = _TextPatterns[type] + _RawInfo[type];
        }

        private void _UpdateAll()
        {
            _UpdateByType(InfoData.SELF_HEALTH);
            _UpdateByType(InfoData.SELF_ENERGY);
            _UpdateByType(InfoData.ENEMY_HEALTH);
            _UpdateByType(InfoData.ENEMY_ENERGY);
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

        private void _SetAll(float selfHealth, float selfEnergy, float enemyHealth, float enemyEnergy)
        {
            SetInfoTo(InfoData.SELF_HEALTH, selfHealth);
            SetInfoTo(InfoData.SELF_ENERGY, selfEnergy);
            SetInfoTo(InfoData.ENEMY_HEALTH, enemyHealth);
            SetInfoTo(InfoData.ENEMY_ENERGY, enemyEnergy);
            _UpdateAll();
        }

        public void Init(float selfHealth = 0.0f, float selfEnergy = 0.0f, float enemyHealth = 0.0f, float enemyEnergy = 0.0f)
        {
            if (!_IsInit)
            {
                _SetAll(selfHealth, selfEnergy, enemyHealth, enemyEnergy);
                _IsInit = true;
            }
            else
            {
                Debug.LogWarning("Repeated init, invalid oeration.");
            }
        }
    }
}
