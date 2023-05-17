using UnityEngine;

namespace Items.StatusData
{
    [System.Serializable]
    public class ArmorStatus
    {
        [SerializeField]
        public StatBonusData statBonuses;
        public int defense;
    }
}