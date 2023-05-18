using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public class NecklaceStatus
    {
        [SerializeField] 
        public StatBonusData statBonuses;
        
        // Copy constructor
        public NecklaceStatus(NecklaceStatus other)
        {
            this.statBonuses = other.statBonuses;
        }
    }
}