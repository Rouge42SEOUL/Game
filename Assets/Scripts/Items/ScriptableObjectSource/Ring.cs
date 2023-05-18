using Items.StatusData;
using UnityEngine;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "Ring Data", menuName = "Inventory/Ring")]
    public class Ring : Equipment
    {
        public RingStatus status;
        
        public override Equipment Equip(Slot slot)
        {
            if (slot == null)
            {
                Debug.LogError("Equip Error: slot is null");
                return null;
            }

            if (slot.slotRing[0] != null && slot.slotRing[1] == null)
            {
                slot.slotRing[1] = this;
                return null;
            }
            
            /* main processing */
            Equipment prev = UnEquip(slot); 
            slot.slotRing[0] = this;
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
            Equipment prev = slot.slotRing[0];
            slot.slotRing[0] = null;
            return prev;
        }
        
        public override Equipment DeepCopy()
        {
            var copy = ScriptableObject.CreateInstance<Ring>();

            copy.itemName = this.itemName;
            copy.description = this.description;
            copy.icon = this.icon;
            copy.gold = this.gold;
            copy.id = this.id;
            copy.reinforcement = this.reinforcement;
            copy.status = new RingStatus(this.status);
            
            return copy;
        }
    }
}