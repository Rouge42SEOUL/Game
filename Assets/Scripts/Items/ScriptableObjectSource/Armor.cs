using Items.StatusData;
using UnityEngine;
using System.Text;
using Actor.Stats;

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
            sb.AppendLine($"Health: {status.health}");
            sb.AppendLine($"Defense: {status.defense}");
            sb.AppendLine();
            sb.AppendLine($"Gold: {gold}");
            
            return sb.ToString();
        }
        
        public override Equipment Equip(Slot slot, PlayerStatObject playerStatObject)
        {
            if (slot == null)
            {
                Debug.LogError("Equip Error: slot is null");
                return null;
            }
            
            Equipment previousArmor = UnEquip(slot, playerStatObject);
            slot.slotArmor = this;
            playerStatObject.AddAttribute(AttributeType.Health, status.health);
            playerStatObject.AddAttribute(AttributeType.Defense, status.defense);
            return previousArmor;
        }

        public override Equipment UnEquip(Slot slot, PlayerStatObject playerStatObject)
        {
            if (slot == null)
            {
                Debug.LogError("UnEquip Error: slot is null");
                return null;
            }
            
            Armor previousArmor = slot.slotArmor;
            slot.slotArmor = null;
            if (previousArmor != null)
            {
                playerStatObject.SubAttribute(AttributeType.Health, previousArmor.status.health);
                playerStatObject.SubAttribute(AttributeType.Defense, previousArmor.status.defense);
            }
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