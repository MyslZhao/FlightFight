using UnityEngine;

using FlightFight.Shared.Enums;

namespace FlightFight.GamePlay.Managers
{
    public class DataTransManager: MonoBehaviour
    {
        private static DataTransManager Instance;

        [Header("Debug模式")]
        public bool _EnableDebug;

        [Header("玩家配置")]
        [SerializeField] private static AmmoEnum[] _SelfAmmo = new AmmoEnum[5];

        [SerializeField] private static AmmoEnum[] _EnemyAmmo = new AmmoEnum[5];

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static AmmoEnum[] SelfAmmo => _SelfAmmo;
        
        public static AmmoEnum[] EnemyAmmo => _EnemyAmmo;

    }

}
