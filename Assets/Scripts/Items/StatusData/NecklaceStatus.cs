using UnityEngine;
using UnityEngine.Serialization;

namespace Items.StatusData
{
    [System.Serializable]
    public class NecklaceStatus
    {
        [FormerlySerializedAs("movespeed")] [SerializeField]
        // public StatBonusData statBonuses;
        public float moveSpeed;
        public float avoidance;
        
        // Copy constructor
        public NecklaceStatus(NecklaceStatus other)
        {
            this.moveSpeed = other.moveSpeed;
            this.avoidance = other.avoidance;
        }
    }
}