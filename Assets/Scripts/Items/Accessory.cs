using Items.StatusData;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Accessory Data", menuName = "Inventory/Accessory")]
    public class Accessory : Equipment
    {
        public AccessoryType type;
        public AccessoryStatus status;
        public override Equipment Equip(Slot slot)
        {
            if (slot == null)
            {
                Debug.LogError("Equip Error: slot is null");
                return null;
            }

            if (type == AccessoryType.Ring && slot.slotRing[1] == null)
            {
                slot.slotRing[1] = this;
                return null;
            }
            
            /* 예외 이후에 main 처리문 */
            Equipment prev = UnEquip(slot);
            switch (type)
            {
                case AccessoryType.Necklace:
                    slot.slotNecklace = this;
                    break;
                case AccessoryType.Ring:
                    slot.slotRing[0] = this;
                    break;
            }
            return prev;
        }

        public override Equipment UnEquip(Slot slot)
        {
            Equipment prev = null;

            if (slot == null)
            {
                Debug.LogError("UnEquip Error: slot is null");
                return null;
            }

            switch (type)
            {
                case AccessoryType.Necklace:
                    prev = slot.slotNecklace;
                    slot.slotNecklace = null;
                    break;
                case AccessoryType.Ring:
                    prev = slot.slotRing[0];
                    slot.slotRing[0] = null;
                    break;
            }
            return prev;
        }
    }
}