using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Items.ScriptableObjectSource;
using Items.init;
using Unity.VisualScripting;

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
        
        /* for blacksmith event */
        public List<Equipment> RequireTotalEquipments()
        {
            HashSet<Equipment> totalEquipmentsSet = new HashSet<Equipment>();
    
            foreach (var weapon in slot.slotWeapon)
            {
                if (weapon != null) 
                    totalEquipmentsSet.Add(weapon);
            }

            if (slot.slotArmor != null) 
                totalEquipmentsSet.Add(slot.slotArmor);

            if (slot.slotNecklace != null) 
                totalEquipmentsSet.Add(slot.slotNecklace);
    
            foreach (var ring in slot.slotRing)
            {
                if (ring != null) 
                    totalEquipmentsSet.Add(ring);
            }
    
            foreach (var item in inventoryItems.OfType<Equipment>())
            {
                if (item != null) 
                    totalEquipmentsSet.Add(item);
            }
    
            return totalEquipmentsSet.ToList();
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
                if (equipmentDatabase.items[id] is Equipment equipment)
                {
                    // Deep copy
                    inventoryItems[idx] = equipment.DeepCopy();
                    UpdateInventory();
                }
                else
                {
                    inventoryItems[idx] = equipmentDatabase.items[id];
                }
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