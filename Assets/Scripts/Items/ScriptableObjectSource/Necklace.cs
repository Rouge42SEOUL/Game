using Items.StatusData;
using UnityEngine;
using System.Text;
using Actor.Stats;

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
            sb.AppendLine($"Move Speed: {status.moveSpeed}");
            sb.AppendLine($"Avoidance: {status.avoidance}");
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
            
            Equipment previousNecklace = UnEquip(slot, playerStatObject);
            slot.slotNecklace = this;
            playerStatObject.AddAttribute(AttributeType.MoveSpeed, status.moveSpeed);
            playerStatObject.AddAttribute(AttributeType.Avoidance, status.avoidance);
            
            return previousNecklace;
        }

        public override Equipment UnEquip(Slot slot, PlayerStatObject playerStatObject)
        {
            if (slot == null)
            {
                Debug.LogError("UnEquip Error: slot is null");
                return null;
            }

            Necklace previousNecklace = slot.slotNecklace;
            slot.slotNecklace = null;
            if (previousNecklace != null)
            {
                playerStatObject.SubAttribute(AttributeType.MoveSpeed, previousNecklace.status.moveSpeed);
                playerStatObject.SubAttribute(AttributeType.Avoidance, previousNecklace.status.avoidance);
            }
            return previousNecklace;
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
