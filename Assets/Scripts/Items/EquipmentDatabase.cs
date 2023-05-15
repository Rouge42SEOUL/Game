using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "EquipmentDatabase", menuName = "Inventory/EquipmentDatabase")]
    public class EquipmentDatabase : ScriptableObject
    {
        public List<Item> items;
    }
}