using System.Collections.Generic;
using Items;
using Items.init;
using Items.ScriptableObjectSource;
using UnityEngine;
using Random = UnityEngine.Random;

public class MerchantUI : EventUI
{ 
    [SerializeField] private EquipmentDatabase productList;
    private GameObject[] _options;
    private Inventory _inventory;
    public List<GameObject> inventoryPanels;
    public List<GameObject> slotPanels;
    
    protected override void Start()
    {
        base.Start();
        _inventory = Inventory.Instance;
        Transform childTransform = transform.Find("Options");
        MerchantOption[] childrenTransforms = childTransform.GetComponentsInChildren<MerchantOption>();
        _options = new GameObject[childrenTransforms.Length];
        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            _options[i] = childrenTransforms[i].gameObject;
        }
        // 인벤토리 참조 코드
        InitBoxes initBoxes = new InitBoxes();
        initBoxes.InitInventoryBoxes(inventoryPanels, "MerchantInventory");
        initBoxes.InitSlotBoxes(slotPanels, "MerchantSlot");
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (_inventory)
        {
            UpdateSlot();
            UpdateInventory();
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < _inventory.inventoryItems.Count; i++)
        {
            InventoryBoxPanel panel = inventoryPanels[i].GetComponent<InventoryBoxPanel>();
            panel.UpdateItem(_inventory.inventoryItems[i]);
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
                    panel.UpdateItem(_inventory.slot.slotWeapon[0]);
                    break;
                case "WeaponSlotBlock1":
                    panel.UpdateItem(_inventory.slot.slotWeapon[1]);
                    break;
                case "ArmorSlotBlock":
                    panel.UpdateItem(_inventory.slot.slotArmor);
                    break;
                case "NecklaceSlotBlock":
                    panel.UpdateItem(_inventory.slot.slotNecklace);
                    break;
                case "RingSlotBlock0":
                    panel.UpdateItem(_inventory.slot.slotRing[0]);
                    break;
                case "RingSlotBlock1":
                    panel.UpdateItem(_inventory.slot.slotRing[1]);
                    break;
                default:
                    Debug.LogError("None of Block is correct");
                    break;
            }
        }
    }

    public void OptionRandomSetting()
    {
        List<Item> items = new List<Item>(productList.items);
        for (int i = 0; i < _options.Length; i++)
        {
            if (i < items.Count)
            {
                int randomValue = Random.Range(0, items.Count);
                _options[i].SetActive(true);
                _options[i].GetComponent<MerchantOption>().Item = items[randomValue];
                items.RemoveAt(randomValue);
            }
            else
            {
                _options[i].SetActive(false);
            }
        }

    }
}
