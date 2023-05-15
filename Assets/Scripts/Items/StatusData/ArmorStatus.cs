using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public struct ArmorStatus
    {
        [SerializeField]
        public StatBonusData statBonuses;
        public int defense;
    }
}