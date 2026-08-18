using System.Linq;

using FlightFight.Shared.Enums;

namespace FlightFight.GamePlay.Ammo
{
    public class AmmoGroup
    {
        private AmmoSlot[] _BulletSlots = new AmmoSlot[5];

        private AmmoSlot _LoadedAmmoSlot => _BulletSlots[_SlotCounter];
        
        private int _SlotCounter;

        public AmmoEnum LoadedAmmo => _LoadedAmmoSlot.Ammo;

        public AmmoGroup(AmmoEnum[] ammoList = null)
        {
            foreach (var i in Enumerable.Range(0, 5))
            {
                _BulletSlots[i] = new AmmoSlot(ammoList[i]);
            }
        }

        public void Consume()
        {
            if (_LoadedAmmoSlot.Number <= 0) return;

            _LoadedAmmoSlot.Consume();

            if (_LoadedAmmoSlot.Number == 0)
            {
                _SlotCounter += 1;
                _SlotCounter %= 5;
            }
        }
    }

}