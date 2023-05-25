namespace Items.StatusData
{
    [System.Serializable]
    public class WeaponStatus
    {
        // public StatBonusData statBonuses;
        public float attackSpeed;
        // public float range;
        public int damage;
        public int health;
        public int defense;
        
        public WeaponStatus(WeaponStatus other)
        {
            // this.statBonuses = other.statBonuses;
            this.attackSpeed = other.attackSpeed;
            // this.range = other.range;
            this.damage = other.damage;
            health = other.health;
            defense = other.defense;
        }
    }
}