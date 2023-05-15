using System.Collections.Generic;
using System.Net.Mime;
using System.IO;
using UnityEngine;
using Items.init;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        public List<GameObject> inventoryPanels;
        public List<GameObject> slotPanels;
        public EquipmentDatabase equipmentDatabase;
        public List<Item> inventoryItems;
        public GameObject inventoryUI;
        public Slot slot;

        private void Awake()
        {
            inventoryItems = new List<Item>(new Equipment[16]);
            slot = gameObject.AddComponent<Slot>();
            InitBoxes.InitInventoryBoxes(inventoryPanels);
            InitBoxes.InitSlotBoxes(slotPanels);
        }

        private void Update()
        {
            // 예를 들어, 'I' 키를 눌렀을 때 인벤토리를 표시하거나 숨기려면 아래 코드를 사용합니다.
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }
        
        private void ToggleInventory()
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        
        public bool EquipItem(int index)
        {
            if (index >= 0 && index < inventoryItems.Count)
            {
                Item itemToEquip = inventoryItems[index];

                if (itemToEquip is Equipment equipment)
                {
                    Equipment previousItem = equipment.UnEquip(slot);
                    equipment.Equip(slot); // Equip the new item in the slot
                    inventoryItems[index] = previousItem; // Move the previous item back to the inventory
                    UpdateInventory();
                    UpdateSlot();
                    return true;
                }
            }
            Debug.LogError("Can't find the type of equipment");
            return false;
        }
        
        public Item AddItem(int id)
        {
            int idx;

            // when check the inventoryItems null
            if (inventoryItems == null)
            {
                Debug.LogError("Inventory items not initialized.");
                return null;
            }
            
            for (idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                    break;
                if (idx == 15)
                {
                    Debug.Log("Inventory is full");
                    return null;
                }
            }
            
            if (id >= 0 && id < equipmentDatabase.items.Count)
            {
                inventoryItems[idx] = equipmentDatabase.items[id];
                UpdateInventory();
            }
            return null;
        }
        private void UpdateInventory()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                InventoryBoxPanel panel = inventoryPanels[i].GetComponent<InventoryBoxPanel>();
                panel.UpdateItem(inventoryItems[i]);
            }
        }

        private void UpdateSlot()
        {
            foreach (GameObject slotPanel in slotPanels)
            {
                InventoryBoxPanel panel = slotPanel.GetComponent<InventoryBoxPanel>();
                switch (panel.name)
                {
                    case "WeaponSlotBlock":
                        panel.UpdateItem(slot.slotWeapon);
                        break;
                    case "ArmorSlotBlock":
                        panel.UpdateItem(slot.slotArmor);
                        break;
                    case "AccessorySlotBlock":
                        panel.UpdateItem(slot.slotAccessory);
                        break;
                }
            }
        }
    }
}