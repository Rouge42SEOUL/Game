using Items.StatusData;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Accessory Data", menuName = "Inventory/Accessory")]
    public class Accessory : Equipment
    {
        public AccessoryStatus status;
        public override void Equip(Slot slot)
        {
            slot.slotAccessory = this;
        }

        public override Equipment UnEquip(Slot slot)
        {
            Accessory previousAccessory = slot.slotAccessory;
            slot.slotAccessory = null;
            return previousAccessory;
        }
    }
}