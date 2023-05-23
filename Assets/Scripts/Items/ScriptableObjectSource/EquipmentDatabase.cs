using System.Collections.Generic;
using UnityEngine;

namespace Items.ScriptableObjectSource
{
    [CreateAssetMenu(fileName = "EquipmentDatabase", menuName = "Scriptable Object/Inventory/EquipmentDatabase")]
    public class EquipmentDatabase : ScriptableObject
    {
        public List<Item> items;
    }
}