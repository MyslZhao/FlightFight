using System.Diagnostics;
using FlightFight.Shared.Enums;

namespace FlightFight.GamePlay.Ammo
{
    internal class AmmoSlot
    {
        private readonly AmmoEnum _Ammo;

        private readonly int _Storage;

        private int _Number;

        public AmmoEnum Ammo => _Ammo;
        
        public int Storage => _Storage;

        public AmmoSlot(AmmoEnum ammo, int storage)
        {
            _Ammo = ammo;
            _Storage = storage;
            _Number = _Storage;
        }

        internal bool TryConsume()
        {
            _Number -= 1;

            Debug.Assert(_Number >= 0, "Wow your '_Number' is blow than zero!");

            if (_Number == 0)
            {
                _Number = _Storage;
                return true;
            }
            return false;
        }
    }

}
