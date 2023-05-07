using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public EquipmentDatabase equipmentDatabase;

    public int inventorySize = 16;
    public List<Equipment> inventoryItems;
    private void Start()
    {
        inventoryItems = new List<Equipment>(new Equipment[inventorySize]);
    }
    public Armor GetArmor(int index)
    {
        if (index >= 0 && index < equipmentDatabase.armors.Count)
        {
            return equipmentDatabase.armors[index];
        }
        return null;
    }

    public Weapon GetWeapon(int index)
    {
        if (index >= 0 && index < equipmentDatabase.weapons.Count)
        {
            return equipmentDatabase.weapons[index];
        }
        return null;
    }
}