using UnityEngine;
using System.Collections.Generic;

// [CreateAssetMenu(fileName = "Item Data", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public void Destroy() {}
}

public abstract class Equipment : Item
{
    public int id;
    public Dictionary<StatType, int> StatBonus;
    public HashSet<ClassType> RequiredClass;
    public abstract bool Equip();
    public abstract bool Unequip();
}

