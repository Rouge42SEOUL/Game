using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public class ArmorStatus
    {
        [SerializeField]
        // public StatBonusData statBonuses;
        public int health;
        public int defense;
        
        // Copy constructor
        public ArmorStatus(ArmorStatus other)
        {
            this.health = other.health;
            this.defense = other.defense;
        }
    }
}