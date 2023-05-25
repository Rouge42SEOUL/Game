using System.Collections.Generic;
using Items.ScriptableObjectSource;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class PlayerData
    {
        public List<Item> inventoryItems;
        public Weapon[] slotWeapon = new Weapon[2];
        public Armor slotArmor;
        public Necklace slotNecklace;
        public Ring[] slotRing = new Ring[2];

        public PlayerData(SaveClass inventory, Slot slot)
        {
            // Deep copy
            inventoryItems = new List<Item>(inventory.InventoryItems);
            slotWeapon = (Weapon[])slot.slotWeapon.Clone();
            slotArmor = slot.slotArmor;
            slotNecklace = slot.slotNecklace;
            slotRing = (Ring[])slot.slotRing.Clone();
        }
    }

    public class SaveClass
    {
        public List<Item> InventoryItems;
        public Slot Slot;

        public void Save()
        {
            PlayerData data = new PlayerData(this, Slot.Instance); // You need to change this line based on your class
            string json = JsonUtility.ToJson(data);

            // 파일에 JSON 문자열 쓰기
            System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        public void Load()
        {
            // 파일에서 JSON 문자열 읽기
            string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/savefile.json");

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // 불러온 데이터로 변수 설정
            InventoryItems = new List<Item>(data.inventoryItems);
            Slot.slotWeapon = (Weapon[])data.slotWeapon.Clone();
            Slot.slotArmor = data.slotArmor;
            Slot.slotNecklace = data.slotNecklace;
            Slot.slotRing = (Ring[])data.slotRing.Clone();

            Inventory.Instance.UpdateInventory();
            Inventory.Instance.UpdateSlot();
        }
    }
}
