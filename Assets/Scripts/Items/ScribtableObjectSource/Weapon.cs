using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{
    public float attackSpeed;
    public float range;
    public int damage;
    public float cooldownReduction;
    public float criticalChance;
    public float accuracy;
    public AttributeTypes weaponAttributes;
    public Dictionary<StatusEffect, float> Effect;
    public HandSide hands;
    public override bool Equip()
    {
        Debug.Log("무기를 장착합니다.");
        return true;
    }

    public override bool Unequip()
    {
        Debug.Log("무기를 해제합니다.");
        return true;
    }
}
