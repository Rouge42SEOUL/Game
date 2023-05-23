using Items.StatusData;
using UnityEngine;
using System.Text;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Inventory/Armor")]
    public class Armor : Equipment
    {
        public ArmorStatus status;
        
        public override string ItemDescription()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{itemName}");
            sb.AppendLine($"(Level {reinforcement})");
            sb.AppendLine();
            sb.AppendLine($"Defense: {status.defense}");
            sb.AppendLine($"Power: {status.statBonuses.power}");
            sb.AppendLine($"Health: {status.statBonuses.health}");
            sb.AppendLine($"Speed: {status.statBonuses.speed}");
            sb.AppendLine();
            sb.AppendLine($"Gold: {gold}");
            
            return sb.ToString();
        }
        
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