using System.Text;
using Actor.Player;
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

        public void Use()
        {
            // /* 수정 필요 */
            // GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            // if (playerObject != null)
            // {
            //     Player player = playerObject.GetComponent<Player>();
            //     if (player != null)
            //     {
            //         player.Heal(healingAmount);
            //     }
            // }
        }
    }
}