using Items.StatusData;
using UnityEngine;

namespace Items
{
    public class Slot: MonoBehaviour
    {
        public Weapon[] slotWeapon = new Weapon[2];
        public Armor slotArmor;
        public Accessory slotNecklace;
        public Accessory[] slotRing = new Accessory[2];

        public TotalStatus RequireTotalValue()
        {
            TotalStatus tot = new TotalStatus();

            foreach (Weapon weapon in slotWeapon)
            {
                if (weapon != null)
                {
                    tot.AddWeaponStatus(weapon.status);
                }
            }
            
            if (slotArmor != null)
            {
                tot.AddArmorStatus(slotArmor.status);
            }

            if (slotNecklace != null)
            {
                tot.AddAccessoryStatus(slotNecklace.status);
            }

            foreach (Accessory accessory in slotRing)
            {
                if (accessory != null)
                {
                    tot.AddAccessoryStatus(accessory.status);
                }
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