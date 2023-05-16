using UnityEngine;

namespace Items
{
    public abstract class Item : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite icon;
    }

    public abstract class Equipment : Item
    {
        public int id;
        public abstract void Equip(Slot slot);
        public abstract Equipment UnEquip(Slot slot);
    }
}
