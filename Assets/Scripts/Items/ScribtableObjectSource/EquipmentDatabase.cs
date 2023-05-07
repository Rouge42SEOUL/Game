using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentDatabase", menuName = "Inventory/EquipmentDatabase")]
public class EquipmentDatabase : ScriptableObject
{
    public List<Armor> armors;
    public List<Weapon> weapons;
}
