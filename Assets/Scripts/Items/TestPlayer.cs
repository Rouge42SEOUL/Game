using System.Collections.Generic;
using Items.ScriptableObjectSource;
using UnityEngine;

namespace Items
{
    public class TestPlayer : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = gameObject.GetComponent<Inventory>();
        }

        void Start()
        {
            _inventory.AddItem(4);
            _inventory.AddItem(5);
            _inventory.AddItem(3);
            _inventory.AddItem(2);
            _inventory.AddItem(6);
            _inventory.AddItem(7);
            _inventory.AddItem(7);
            _inventory.AddItem(7);
            _inventory.AddItem(7);
            _inventory.Equip(0);
            _inventory.Equip(3);
            _inventory.Equip(4);
            _inventory.Equip(5);
            List<Equipment> totalEquipments = _inventory.RequireTotalEquipments();
            // Debug.LogError(totalEquipments.Count);
            _inventory.slot.LogTotalStatus();
        }
    }
}