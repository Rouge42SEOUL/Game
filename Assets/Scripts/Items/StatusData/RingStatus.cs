using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public class RingStatus
    {
        [SerializeField]
        // public StatBonusData statBonuses;
        public float accuracy;
        public float attackSpeed;

        // Copy constructor
        public RingStatus(RingStatus other)
        {
            this.accuracy = other.accuracy;
            attackSpeed = other.attackSpeed;
        }
    }
}