using FlightFight.Shared.Enums;

namespace FlightFight.Shared.Data
{
    public readonly struct BulletInitData
    {
        public PlaneIdentity Identity { get; }
        public AmmoEnum Type { get; }
        public float Speed { get; }
        public float LastTime { get; }

        public BulletInitData(PlaneIdentity identity, AmmoEnum type, float speed, float lastTime)
        {
            Identity = identity;
            Type = type;
            Speed = speed;
            LastTime = lastTime;
        }
    }
}
