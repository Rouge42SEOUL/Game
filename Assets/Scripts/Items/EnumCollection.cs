[System.Serializable]
public struct StatBonusData
{
    public float power;
    public float health;
    public float speed;

    public static StatBonusData operator *(StatBonusData data1, float factor)
    {
        return new StatBonusData
        {
            power = data1.power * factor,
            health = data1.health * factor, 
            speed = data1.speed * factor,
        };
    }
    public static StatBonusData operator +(StatBonusData data1, StatBonusData data2)
    { 
        return new StatBonusData
        {  
            power = data1.power + data2.power, 
            health = data1.health + data2.health, 
            speed = data1.speed + data2.speed,
        };
    }
}

public enum StatType
{
    AttackPower,
    Speed,
    Health
}

public enum SlotType
{
    Weapon0,
    Weapon1,
    Armor,
    Necklace,
    Ring0,
    Ring1,
    Inventory
}

public enum AccessoryType
{
    Ring,
    Necklace
}

public enum WeaponType
{
    OneHand,
    TwoHand,
    Shield,
    None
}

public enum AttributeTypes
{
    Strike,
    Slash,
    Thrust,
    Fire,
    Ice,
    Wind,
    Earth,
    Light,
    Dark
}

public enum StatusEffect
{
    Poison,
    Burn,
    Freeze,
    Fracture,
    Bleeding,
    Blind,
    Confusion,
    Paralysis
}
