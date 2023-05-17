namespace Items.StatusData
{
    [System.Serializable]
    public class TotalStatus
    {
        public StatBonusData statBonuses;
        public float attackSpeed;
        public float range;
        public int damage;
        public int defense;
        
        public void AddWeaponStatus(WeaponStatus weaponStatus)
        {
            if (weaponStatus == null)
                return;
            statBonuses += weaponStatus.statBonuses;
            attackSpeed += weaponStatus.attackSpeed;
            range += weaponStatus.range;
            damage += weaponStatus.damage;
        }

        public void AddArmorStatus(ArmorStatus armorStatus)
        {
            if (armorStatus == null)
                return;
            statBonuses += armorStatus.statBonuses;
            defense += armorStatus.defense;
        }

        public void AddAccessoryStatus(AccessoryStatus accessoryStatus)
        {
            if (accessoryStatus == null)
                return;
            statBonuses += accessoryStatus.statBonuses;
        }
    }
}