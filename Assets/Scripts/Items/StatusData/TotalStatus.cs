// namespace Items.StatusData
// {
//     [System.Serializable]
//     public class TotalStatus
//     {
//         private const float ReinforcementFactor = 0.2f;
//         public StatBonusData statBonuses;
//         public float attackSpeed;
//         public float range;
//         public int damage;
//         public int defense;
//         
//         public void AddWeaponStatus(WeaponStatus weaponStatus)
//         {
//             if (weaponStatus == null)
//                 return;
//             statBonuses += weaponStatus.statBonuses + (weaponStatus.statBonuses * ReinforcementFactor);
//             attackSpeed += weaponStatus.attackSpeed + (weaponStatus.attackSpeed * ReinforcementFactor);
//             range += weaponStatus.range + (weaponStatus.range * ReinforcementFactor);
//             damage += weaponStatus.damage + (int)(weaponStatus.damage * ReinforcementFactor);
//         }
//
//         public void AddArmorStatus(ArmorStatus armorStatus)
//         {
//             if (armorStatus == null)
//                 return;
//             statBonuses += armorStatus.statBonuses + (armorStatus.statBonuses * ReinforcementFactor);
//             defense += armorStatus.defense + (int)(armorStatus.defense * ReinforcementFactor);
//         }
//
//         public void AddNecklaceStatus(NecklaceStatus necklaceStatus)
//         {
//             if (necklaceStatus == null)
//                 return;
//             statBonuses += necklaceStatus.statBonuses + (necklaceStatus.statBonuses * ReinforcementFactor);
//         }
//         
//         public void AddRingStatus(RingStatus ringStatus)
//         {
//             if (ringStatus == null)
//                 return;
//             statBonuses += ringStatus.statBonuses + (ringStatus.statBonuses * ReinforcementFactor);
//         }
//     }
// }