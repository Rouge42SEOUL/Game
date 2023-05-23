using System.Text;
using UnityEngine;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "HealingPotion", menuName = "Scriptable Object/Inventory/HealingPotion")]
    public class HealingPotion : Item
    {
        public int healingAmount; // The amount of health that this potion will restore.

        public override string ItemDescription()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{itemName}");
            sb.AppendLine();
            sb.AppendLine($"Restores {healingAmount}");
            sb.AppendLine();
            sb.AppendLine($"Gold: {gold}");
            
            return sb.ToString();
        }

        // public void Use(Player player)
        // {
        //     player.Health += healingAmount;
        // }
    }
}