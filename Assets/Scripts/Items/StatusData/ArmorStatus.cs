using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public class ArmorStatus
    {
        [SerializeField]
        public StatBonusData statBonuses;
        public int defense;
        
        // Copy constructor
        public ArmorStatus(ArmorStatus other)
        {
            this.statBonuses = other.statBonuses;
            this.defense = other.defense;
        }
    }
}