using Items.StatusData;
using UnityEngine;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "Necklace Data", menuName = "Inventory/Necklace")]
    public class Necklace : Equipment
    {
        public NecklaceStatus status;
        public override Equipment Equip(Slot slot)
        {
            if (slot == null)
            {
                Debug.LogError("Equip Error: slot is null");
                return null;
            }

            /* main processing */
            Equipment prev = UnEquip(slot); 
            slot.slotNecklace = this;
            return prev;
        }

        public override Equipment UnEquip(Slot slot)
        {
            if (slot == null)
            {
                Debug.LogError("UnEquip Error: slot is null");
                return null;
            }
            
            /* main processing */
            Equipment prev = slot.slotNecklace;
            slot.slotNecklace = null;
            return prev;
        }
    }
}