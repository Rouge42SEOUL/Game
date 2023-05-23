using UnityEngine;

namespace Items.ScriptableObjectSource
{
    public abstract class Item : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite icon;
        public int gold;
    }

    public abstract class Equipment : Item
    {
        public int id;
        public int reinforcement;
        
        public abstract Equipment Equip(Slot slot);
        public abstract Equipment UnEquip(Slot slot);
        // Add this:
        public abstract Equipment DeepCopy();
    }
}
