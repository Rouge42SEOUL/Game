using Items.StatusData;
using UnityEngine;

namespace Items
{
    public enum WeaponType
    {
        OneHand,
        TwoHand,
        Shield,
        None
    }
    
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Weapon")]
    public class Weapon : Equipment
    {
        public WeaponStatus status;
        
        public override void Equip(Slot slot)
        {
            slot.slotWeapon = this;
        }

        public override Equipment UnEquip(Slot slot)
        {
            Weapon previousWeapon = slot.slotWeapon;
            slot.slotWeapon = null;
            return previousWeapon;
        }
    }
}