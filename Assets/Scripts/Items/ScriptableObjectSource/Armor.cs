using Items.StatusData;
using UnityEngine;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Armor")]
    public class Armor : Equipment
    {
        public ArmorStatus status;
        
        public override Equipment Equip(Slot slot)
        {
            if (slot == null)
            {
                Debug.LogError("Equip Error: slot is null");
                return null;
            }
            
            Equipment previousArmor = UnEquip(slot);
            slot.slotArmor = this;
            return previousArmor;
        }

        public override Equipment UnEquip(Slot slot)
        {
            if (slot == null)
            {
                Debug.LogError("UnEquip Error: slot is null");
                return null;
            }
            
            Armor previousArmor = slot.slotArmor;
            slot.slotArmor = null;
            return previousArmor;
        }
        
        public override Equipment DeepCopy()
        {
            var copy = ScriptableObject.CreateInstance<Armor>();
            
            copy.itemName = this.itemName;
            copy.description = this.description;
            copy.icon = this.icon;
            copy.gold = this.gold;
            copy.id = this.id;
            copy.reinforcement = this.reinforcement;
            copy.status = new ArmorStatus(this.status);
            
            return copy;
        }
    }
}