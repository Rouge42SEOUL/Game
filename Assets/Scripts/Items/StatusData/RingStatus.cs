using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public class RingStatus
    {
        [SerializeField] 
        public StatBonusData statBonuses;
        
        // Copy constructor
        public RingStatus(RingStatus other)
        {
            this.statBonuses = other.statBonuses;
        }
    }
}