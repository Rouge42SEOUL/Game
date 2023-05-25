using Items.StatusData;
using UnityEngine;
using System.Text;
using Actor.Stats;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "Ring Data", menuName = "Scriptable Object/Inventory/Ring")]
    public class Ring : Equipment
    {
        public RingStatus status;
        
        public override string ItemDescription()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{itemName}");
            sb.AppendLine($"(Level {reinforcement})");
            sb.AppendLine();
            sb.AppendLine($"Attack Speed: {status.attackSpeed}");
            sb.AppendLine($"Accuracy: {status.accuracy}");
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

            if (slot.slotRing[0] != null && slot.slotRing[1] == null)
            {
                slot.slotRing[1] = this;
                playerStatObject.AddAttribute(AttributeType.AttackSpeed, this.status.attackSpeed);
                playerStatObject.AddAttribute(AttributeType.Accuracy, this.status.accuracy);
                return null;
            }
            Equipment previousRing = UnEquip(slot, playerStatObject);
            
            slot.slotRing[0] = this;
            playerStatObject.AddAttribute(AttributeType.AttackSpeed, this.status.attackSpeed);
            playerStatObject.AddAttribute(AttributeType.Accuracy, this.status.accuracy);
            return previousRing;
        }

        public override Equipment UnEquip(Slot slot, PlayerStatObject playerStatObject)
        {
            if (slot == null)
            {
                Debug.LogError("UnEquip Error: slot is null");
                return null;
            }
            
            Ring previousRing = slot.slotRing[0];
            slot.slotRing[0] = null;
            if (previousRing != null)
            {
                playerStatObject.SubAttribute(AttributeType.AttackSpeed, previousRing.status.attackSpeed);
                playerStatObject.SubAttribute(AttributeType.Accuracy, previousRing.status.accuracy);
            }
            return previousRing;
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
