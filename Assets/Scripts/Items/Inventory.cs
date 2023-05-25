using System.Collections.Generic;
using System.Linq;
using Actor.Player;
using Actor.Stats;
using UnityEngine;
using Items.ScriptableObjectSource;
using Items.init;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance { get; private set; }
        public PlayerStatObject playerStatObject;

        public List<GameObject> inventoryPanels;
        public List<GameObject> slotPanels;
        public EquipmentDatabase equipmentDatabase;
        public List<Item> inventoryItems;
        public GameObject inventoryUI;
        public Slot slot;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            } 
            else 
            {
                Instance = this;
            }

            InitBoxes initBoxes = new InitBoxes();
            inventoryItems = new List<Item>(new Equipment[16]);
            slot = gameObject.AddComponent<Slot>();
            initBoxes.InitInventoryBoxes(inventoryPanels, "InventoryBoxes");
            initBoxes.InitSlotBoxes(slotPanels, "SlotBoxes");
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateSlot();
            UpdateInventory();
        }

        public bool ReleasingWeaponItem0()
        {
            Equipment prev = slot.slotWeapon[0];
            
            for (int idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                {
                    playerStatObject.SubAttribute(AttributeType.Attack, slot.slotWeapon[0].status.damage);
                    playerStatObject.SubAttribute(AttributeType.AttackSpeed, slot.slotWeapon[0].status.attackSpeed);
                    playerStatObject.SubAttribute(AttributeType.Health, slot.slotWeapon[0].status.health);
                    playerStatObject.SubAttribute(AttributeType.Defense, slot.slotWeapon[0].status.defense);
                    inventoryItems[idx] = prev;
                    if (slot.slotWeapon[0].type == WeaponType.TwoHand)
                    {
                        slot.slotWeapon[1] = null;
                    }
                    slot.slotWeapon[0] = null;
                    return true;
                }
            }
            Debug.Log("Inventory is full");
            return false;
        }

        public bool EquipItemEvent(int id)
        {
            switch (inventoryItems[id])
            {
                case Equipment equipment:
                    inventoryItems[id] = equipment.Equip(slot, playerStatObject);
                    return true;
                case HealingPotion healingPotion:
                {
                    inventoryItems[id] = null;
                    if (playerStatObject.currentHealthPoint < playerStatObject.baseHealthPoint)
                    {
                        playerStatObject.currentHealthPoint += healingPotion.healingAmount;
                    }
                    return true;
                }
                default:
                    return false;
            }
        }
        public bool ReleasingWeaponItem1()
        {
            Equipment prev = slot.slotWeapon[1];

            for (int idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                {
                    playerStatObject.SubAttribute(AttributeType.Attack, slot.slotWeapon[1].status.damage);
                    playerStatObject.SubAttribute(AttributeType.AttackSpeed, slot.slotWeapon[1].status.attackSpeed);
                    playerStatObject.SubAttribute(AttributeType.Health, slot.slotWeapon[1].status.health);
                    playerStatObject.SubAttribute(AttributeType.Defense, slot.slotWeapon[1].status.defense);
                    inventoryItems[idx] = prev;
                    if (slot.slotWeapon[1].type == WeaponType.TwoHand)
                    {
                        slot.slotWeapon[0] = null;
                    }
                    slot.slotWeapon[1] = null;
                    return true;
                }
            }
            Debug.Log("Inventory is full");
            return false;
        }
        public bool ReleasingArmorItem()
        {
            Equipment prev = slot.slotArmor;
            
            for (int idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                {
                    playerStatObject.SubAttribute(AttributeType.Health, slot.slotArmor.status.health);
                    playerStatObject.SubAttribute(AttributeType.Defense, slot.slotArmor.status.defense);
                    inventoryItems[idx] = prev;
                    slot.slotArmor = null;
                    return true;
                }
            }
            Debug.Log("Inventory is full");
            return false;
        }
        public bool ReleasingRingItem0()
        {
            Equipment prev = slot.slotRing[0];
            
            for (int idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                {
                    playerStatObject.SubAttribute(AttributeType.AttackSpeed, slot.slotRing[0].status.attackSpeed);
                    playerStatObject.SubAttribute(AttributeType.Accuracy, slot.slotRing[0].status.accuracy);
                    inventoryItems[idx] = prev;
                    slot.slotRing[0] = null;
                    return true;
                }
            }
            Debug.Log("Inventory is full");
            return false;
        }
        public bool ReleasingRingItem1()
        {
            Equipment prev = slot.slotRing[1];
            
            for (int idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                {
                    
                    playerStatObject.SubAttribute(AttributeType.AttackSpeed, slot.slotRing[1].status.attackSpeed);
                    playerStatObject.SubAttribute(AttributeType.Accuracy, slot.slotRing[1].status.accuracy);
                    inventoryItems[idx] = prev;
                    slot.slotRing[1] = null;
                    return true;
                }
            }
            Debug.Log("Inventory is full");
            return false;
        }
        public bool ReleasingNecklaceItem()
        {
            Equipment prev = slot.slotNecklace;
            
            for (int idx = 0; idx < 16; idx++)
            {
                if (inventoryItems[idx] == null)
                {
                    playerStatObject.SubAttribute(AttributeType.MoveSpeed, slot.slotNecklace.status.moveSpeed);
                    playerStatObject.SubAttribute(AttributeType.Avoidance, slot.slotNecklace.status.avoidance);
                    inventoryItems[idx] = prev;
                    slot.slotNecklace = null;
                    return true;
                }
            }
            Debug.Log("Inventory is full");
            return false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }
        
        public void ToggleInventory()
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
                    Equipment previousItem = equipment.Equip(slot, playerStatObject);
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
                    // Debug.Log("Inventory is full");
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
        public void UpdateInventory()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                InventoryBoxPanel panel = inventoryPanels[i].GetComponent<InventoryBoxPanel>();
                panel.UpdateItem(inventoryItems[i]);
            }
        }

        public void UpdateSlot()
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
        
        [System.Serializable]
        public class PlayerData
        {
            public List<Item> inventoryItems;
            public Weapon[] slotWeapon = new Weapon[2];
            public Armor slotArmor;
            public Necklace slotNecklace;
            public Ring[] slotRing = new Ring[2];

            public PlayerData(Inventory inventory, Slot slot)
            {
                // Deep copy
                inventoryItems = new List<Item>(inventory.inventoryItems);
                slotWeapon = (Weapon[])slot.slotWeapon.Clone();
                slotArmor = slot.slotArmor;
                slotNecklace = slot.slotNecklace;
                slotRing = (Ring[])slot.slotRing.Clone();
            }
        }

        public void Save()
        {
            PlayerData data = new PlayerData(this, Slot.Instance);
            string json = JsonUtility.ToJson(data);

            // Write JSON string to file
            System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        public void Load()
        {
            // Read JSON string from file
            string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/savefile.json");

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Set variables from loaded data
            inventoryItems = new List<Item>(data.inventoryItems);
            slot.slotWeapon = (Weapon[])data.slotWeapon.Clone();
            slot.slotArmor = data.slotArmor;
            slot.slotNecklace = data.slotNecklace;
            slot.slotRing = (Ring[])data.slotRing.Clone();

            UpdateInventory();
            UpdateSlot();
        }
    }
}