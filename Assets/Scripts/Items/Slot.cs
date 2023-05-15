using Items.StatusData;
using UnityEngine;

namespace Items
{
    public class Slot: MonoBehaviour
    {
        public Weapon slotWeapon;
        public Armor slotArmor;
        public Accessory slotAccessory;

        public TotalStatus RequireTotalValue()
        {
            TotalStatus tot = new TotalStatus();

            if (slotWeapon != null)
            {
                tot.AddWeaponStatus(slotWeapon.status);
            }

            if (slotArmor != null)
            {
                tot.AddArmorStatus(slotArmor.status);
            }

            if (slotAccessory != null)
            {
                tot.AddAccessoryStatus(slotAccessory.status);
            }
            
            return tot;
        }
        
        public void LogTotalStatus()
        {
            TotalStatus totalStatus = RequireTotalValue();
            Debug.Log($"Total Status: StatBonuses - Power: {totalStatus.statBonuses.power}, Health: {totalStatus.statBonuses.health}, Speed: {totalStatus.statBonuses.speed}, AttackSpeed: {totalStatus.attackSpeed}, Range: {totalStatus.range}, Damage: {totalStatus.damage}, Defense: {totalStatus.defense}");
        }
    }
}