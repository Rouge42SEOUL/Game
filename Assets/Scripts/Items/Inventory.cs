using System.Collections.Generic;
using UnityEngine;
using Items.ScriptableObjectSource;
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
            InitBoxes initBoxes = new InitBoxes();
            inventoryItems = new List<Item>(new Equipment[16]);
            slot = gameObject.AddComponent<Slot>();
            initBoxes.InitInventoryBoxes(inventoryPanels);
            initBoxes.InitSlotBoxes(slotPanels);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }
        
        private void ToggleInventory()
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        
        public bool Equip(int index)
        {
            if (index >= 0 && index < inventoryItems.Count)
            {
                Item itemToEquip = inventoryItems[index];
                if (itemToEquip == null)
                    return false;

                if (itemToEquip is Equipment equipment)
                {
                    Equipment previousItem = equipment.Equip(slot);
                    inventoryItems[index] = previousItem;
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
                switch (slotPanel.name)
                {
                    case "WeaponSlotBlock0":
                        panel.UpdateItem(slot.slotWeapon[0]);
                        break;
                    case "WeaponSlotBlock1":
                        panel.UpdateItem(slot.slotWeapon[1]);
                        break;
                    case "ArmorSlotBlock":
                        panel.UpdateItem(slot.slotArmor);
                        break;
                    case "NecklaceSlotBlock":
                        panel.UpdateItem(slot.slotNecklace);
                        break;
                    case "RingSlotBlock0":
                        panel.UpdateItem(slot.slotRing[0]);
                        break;
                    case "RingSlotBlock1":
                        panel.UpdateItem(slot.slotRing[1]);
                        break;
                    default:
                        Debug.LogError("None of Block is correct");
                        break;
                }
            }
        }
    }
}