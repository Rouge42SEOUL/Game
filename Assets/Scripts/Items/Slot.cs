using System.Collections.Generic;
using Items.StatusData;
using UnityEngine;
using Items.ScriptableObjectSource;

namespace Items
{
    public class Slot: MonoBehaviour
    {
        public Weapon[] slotWeapon = new Weapon[2];
        public Armor slotArmor;
        public Necklace slotNecklace;
        public Ring[] slotRing = new Ring[2];

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
                tot.AddNecklaceStatus(slotNecklace.status);
            }

            foreach (Ring ring in slotRing)
            {
                if (ring != null)
                {
                    tot.AddRingStatus(ring.status);
                }
            }
            
            return tot;
        }
        
        /* for blacksmith event */
        public List<Equipment> GetItems()
        {
            List<Equipment> items = new List<Equipment>();
            items.AddRange(slotWeapon);
            items.Add(slotArmor);
            items.Add(slotNecklace);
            items.AddRange(slotRing);

            return items;
        }
        
        public void LogTotalStatus()
        {
            TotalStatus totalStatus = RequireTotalValue();
            Debug.Log($"Total Status: StatBonuses - Power: {totalStatus.statBonuses.power}, Health: {totalStatus.statBonuses.health}, Speed: {totalStatus.statBonuses.speed}, AttackSpeed: {totalStatus.attackSpeed}, Range: {totalStatus.range}, Damage: {totalStatus.damage}, Defense: {totalStatus.defense}");
        }
    }
}