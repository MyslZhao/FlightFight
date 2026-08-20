using System.Diagnostics;
using System.Linq;
using FlightFight.GamePlay.Managers;
using FlightFight.Shared.Data;
using FlightFight.Shared.Enums;

namespace FlightFight.GamePlay.Ammo
{
    public class AmmoGroup
    {
        private readonly AmmoSlot[] _BulletSlots = new AmmoSlot[5];

        private AmmoSlot _LoadedAmmoSlot => _BulletSlots[_SlotCounter];
        
        private int _SlotCounter = 0;

        public AmmoEnum LoadedAmmo => _LoadedAmmoSlot.Ammo;

        public AmmoGroup(AmmoEnum[] ammoList = null)
        {
            foreach (var i in Enumerable.Range(0, 5))
            {
                var _1 = BulletManager.BulletAssets[ammoList[i]];
                _BulletSlots[i] = new AmmoSlot(_1.Name, _1.Storage);
            }
        }

        internal bool TryConsume()
        {
            if (_LoadedAmmoSlot.TryConsume())
            {
                _SlotCounter += 1;
                _SlotCounter %= 5;
                return true;
            }
            return false;
        }
    }

}