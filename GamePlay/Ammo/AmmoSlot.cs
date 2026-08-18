using FlightFight.Shared.Enums;

namespace FlightFight.GamePlay.Ammo
{
    internal class AmmoSlot
    {
        private AmmoEnum _Ammo;

        private int _CurrentNumber;

        private float _currentColddownProcess;

        public AmmoEnum Ammo => _Ammo;

        public int Number => _CurrentNumber;

        public AmmoSlot(AmmoEnum ammo)
        {
            _Ammo = ammo;
        }

        public void Consume()
        {
            if (_CurrentNumber == 0)
                return;
            _CurrentNumber -= 1;
        }
    }

}
