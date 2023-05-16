using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Armor")]
public class Armor : Equipment
{
    // public int defense;
    public Dictionary<StatType, int> StatBonus;
    public float hpRecovery;
    public float cooldownReduction;
    public Dictionary<AttributeTypes, float> Resistance;
    public Dictionary<AttributeTypes, float> Weakness;

    public override bool Equip()
    {
        Debug.Log("방어구를 장착합니다.");
        return true;
    }

    public override bool Unequip()
    {
        Debug.Log("방어구를 해제합니다.");
        return true;
    }
}
