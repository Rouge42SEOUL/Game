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
            _inventory.AddItem(7);
            _inventory.EquipItem(0);
            _inventory.EquipItem(3);
            _inventory.EquipItem(4);
            _inventory.slot.LogTotalStatus();
        }
    }
}