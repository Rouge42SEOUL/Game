using Items.StatusData;
using UnityEngine;

namespace Items
{
        [CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Weapon")]
        public class Weapon : Equipment
        {
            public WeaponType type;
            public WeaponStatus status;
            
            public override Equipment Equip(Slot slot)
            {
                if (slot == null)
                {
                    Debug.LogError("Equip Error: slot is null");
                    return null;
                }

                if (type == WeaponType.OneHand && slot.slotWeapon[1] == null)
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
                switch (type)
                {
                    case WeaponType.OneHand:
                        previousWeapon = slot.slotWeapon[0];
                        slot.slotWeapon[0] = null;
                        break;
                    case WeaponType.TwoHand:
                        previousWeapon = slot.slotWeapon[0];
                        slot.slotWeapon[0] = null;
                        slot.slotWeapon[1] = null;
                        break;
                    case WeaponType.Shield:
                        previousWeapon = slot.slotWeapon[1];
                        slot.slotWeapon[1] = null;
                        break;
                }
                return previousWeapon;
            }
        }
}