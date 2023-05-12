using System.Collections.Generic;
using Items.StatusData;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Armor")]
    public class Armor : Equipment
    {
        public ArmorStatus status;
        public override void Equip(Slot slot)
        {
            slot.slotArmor = this;
        }

        public override Equipment UnEquip(Slot slot)
        {
            Armor previousArmor = slot.slotArmor;
            slot.slotArmor = null;
            return previousArmor;
        }
    }
}