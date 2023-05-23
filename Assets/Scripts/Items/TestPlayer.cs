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
            _inventory.AddItem(33);
            for (int i = 0; i < 32; i++)
            {
                if (i % 2 == 0)
                {
                    _inventory.AddItem(i);
                }
            }
            List<Equipment> totalEquipments = _inventory.RequireTotalEquipments();
            Debug.LogError(totalEquipments.Count);
            _inventory.slot.LogTotalStatus();
        }
    }
}