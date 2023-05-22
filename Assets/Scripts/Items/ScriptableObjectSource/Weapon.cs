using Items.StatusData;
using UnityEngine;
using System.Text;

namespace Items.ScriptableObjectSource
{
        [CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Weapon")]
        public class Weapon : Equipment
        {
            public WeaponType type;
            public WeaponStatus status;
            
            public override string ItemDescription()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"{itemName}");
                sb.AppendLine($"(Level {reinforcement})");
                sb.AppendLine();
                sb.AppendLine($"Damage: {status.damage}");
                sb.AppendLine($"Range: {status.range}");
                sb.AppendLine($"Attack Speed: {status.attackSpeed}");
                sb.AppendLine($"Power: {status.statBonuses.power}");
                sb.AppendLine($"Health: {status.statBonuses.health}");
                sb.AppendLine($"Speed: {status.statBonuses.speed}");

                return sb.ToString();
            }
            
            public override Equipment Equip(Slot slot)
            {
                if (slot == null)
                {
                    Debug.LogError("Equip Error: slot is null");
                    return null;
                }

                if (type == WeaponType.OneHand && slot.slotWeapon[0] != null && slot.slotWeapon[1] == null)
                { 
                    slot.slotWeapon[1] = this;
                    return null;
                }

                /* 예외 이후에 main 처리문 */
                Equipment previousWeapon = UnEquip(slot);
                switch (type)
                {
                    case WeaponType.OneHand:
                        slot.slotWeapon[0] = this;
                        break;
                    case WeaponType.Shield:
                        slot.slotWeapon[1] = this;
                        break;
                    case WeaponType.TwoHand:
                        slot.slotWeapon[0] = this;
                        slot.slotWeapon[1] = this;
                        break;
                }

                return previousWeapon;
            }

            public override Equipment UnEquip(Slot slot)
            {
                Equipment previousWeapon = null;

                if (slot == null)
                {
                    Debug.LogError("UnEquip Error: slot is null");
                    return null;
                }

                if (slot.slotWeapon[0] != null && slot.slotWeapon[0].type == WeaponType.TwoHand)
                {
                    previousWeapon = slot.slotWeapon[0];
                    slot.slotWeapon[0] = null;
                    slot.slotWeapon[1] = null;
                    return previousWeapon;
                }
                
                switch (type)
                {
                    case WeaponType.OneHand:
                        previousWeapon = slot.slotWeapon[0];
                        slot.slotWeapon[0] = null;
                        break;
                    case WeaponType.TwoHand:
                        Inventory inventory = Inventory.Instance;
                        if (slot.slotWeapon[0] != null)
                        {
                            inventory.ReleasingWeaponItem0();
                        }

                        if (slot.slotWeapon[1] != null)
                        {
                            inventory.ReleasingWeaponItem1();
                        }
                        
                        inventory.UpdateInventory();
                        inventory.UpdateSlot();
                        break;
                    case WeaponType.Shield:
                        previousWeapon = slot.slotWeapon[1];
                        slot.slotWeapon[1] = null;
                        break;
                }
                return previousWeapon;
            }
            
            public override Equipment DeepCopy()
            {
                var copy = ScriptableObject.CreateInstance<Weapon>();

                copy.itemName = this.itemName;
                copy.description = this.description;
                copy.icon = this.icon;
                copy.gold = this.gold;
                copy.id = this.id;
                copy.reinforcement = this.reinforcement;
                copy.status = new WeaponStatus(this.status);
                copy.type = this.type;
                
                return copy;
            }
        }
}