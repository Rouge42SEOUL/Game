using Items.StatusData;
using UnityEngine;
using System.Text;
using Actor.Stats;

namespace Items.ScriptableObjectSource
{
        [CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Inventory/Weapon")]
        public class Weapon : Equipment
        {
            public WeaponType type;
            public WeaponStatus status;
            
            public override string ItemDescription()
            {
                StringBuilder sb = new StringBuilder();

                if (type == WeaponType.Shield)
                {
                    sb.AppendLine($"{itemName}");
                    sb.AppendLine($"(Level {reinforcement})");
                    sb.AppendLine();
                    sb.AppendLine($"Health: {status.health}");
                    sb.AppendLine($"Defense: {status.defense}");
                    sb.AppendLine();
                    sb.AppendLine($"Gold: {gold}");

                }
                else
                {
                    sb.AppendLine($"{itemName}");
                    sb.AppendLine($"(Level {reinforcement})");
                    sb.AppendLine();
                    sb.AppendLine($"Damage: {status.damage}");
                    sb.AppendLine($"Attack Speed: {status.attackSpeed}");
                    sb.AppendLine();
                    sb.AppendLine($"Gold: {gold}");

                }
                
                return sb.ToString();
            }
            
            public override Equipment Equip(Slot slot, PlayerStatObject playerStatObject)
            {
                if (slot == null)
                {
                    Debug.LogError("Equip Error: slot is null");
                    return null;
                }

                if (type == WeaponType.OneHand && slot.slotWeapon[0] != null && slot.slotWeapon[1] == null)
                { 
                    slot.slotWeapon[1] = this;
                    playerStatObject.AddAttribute(AttributeType.Attack, status.damage);
                    playerStatObject.AddAttribute(AttributeType.AttackSpeed, status.attackSpeed);
                    return null;
                }

                /* 예외 이후에 main 처리문 */
                Equipment previousWeapon = UnEquip(slot, playerStatObject);
                switch (type)
                {
                    case WeaponType.OneHand:
                        slot.slotWeapon[0] = this;
                        playerStatObject.AddAttribute(AttributeType.Attack, status.damage);
                        playerStatObject.AddAttribute(AttributeType.AttackSpeed, status.attackSpeed);
                        break;
                    case WeaponType.Shield:
                        slot.slotWeapon[1] = this;
                        playerStatObject.AddAttribute(AttributeType.Health, status.health);
                        playerStatObject.AddAttribute(AttributeType.Defense, status.defense);
                        break;
                    case WeaponType.TwoHand:
                        slot.slotWeapon[0] = this;
                        slot.slotWeapon[1] = this;
                        playerStatObject.AddAttribute(AttributeType.Attack, status.damage);
                        playerStatObject.AddAttribute(AttributeType.AttackSpeed, status.attackSpeed);
                        break;
                }

                return previousWeapon;
            }

            public override Equipment UnEquip(Slot slot, PlayerStatObject playerStatObject)
            {
                Weapon previousWeapon = null;

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
                    playerStatObject.SubAttribute(AttributeType.Attack, previousWeapon.status.damage);
                    playerStatObject.SubAttribute(AttributeType.AttackSpeed, previousWeapon.status.attackSpeed);
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

                if (previousWeapon != null)
                {
                    playerStatObject.SubAttribute(AttributeType.Attack, previousWeapon.status.damage);
                    playerStatObject.SubAttribute(AttributeType.AttackSpeed, previousWeapon.status.attackSpeed);
                    playerStatObject.SubAttribute(AttributeType.Health, previousWeapon.status.health);
                    playerStatObject.SubAttribute(AttributeType.Defense, previousWeapon.status.defense);
                }

                return previousWeapon;
            }
            
            public override Equipment DeepCopy()
            {
                var copy = CreateInstance<Weapon>();

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