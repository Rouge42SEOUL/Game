using Items.StatusData;
using UnityEngine;
using System.Text;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "Necklace Data", menuName = "Scriptable Object/Inventory/Necklace")]
    public class Necklace : Equipment
    {
        public NecklaceStatus status;
        
        public override string ItemDescription()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{itemName}");
            sb.AppendLine($"(Level {reinforcement})");
            sb.AppendLine();
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
        
        public override Equipment DeepCopy()
        {
            var copy = ScriptableObject.CreateInstance<Necklace>();

            copy.itemName = this.itemName;
            copy.description = this.description;
            copy.icon = this.icon;
            copy.gold = this.gold;
            copy.id = this.id;
            copy.reinforcement = this.reinforcement;
            copy.status = new NecklaceStatus(this.status);
            
            return copy;
        }
    }
}