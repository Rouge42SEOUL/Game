using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent.SlotClick
{
    public class EventClickSlotWeapon1 : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;
        public MerchantUI merchantUI;

        private void Start()
        {
            _inventory = Inventory.Instance;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _inventory.Save();
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_inventory == null || _inventory.slot == null || _inventory.slot.slotWeapon[1] == null)
                    return;
                _inventory.ReleasingWeaponItem1();
                _inventory.UpdateInventory();
                _inventory.UpdateSlot();
                if (merchantUI == null) return;
                merchantUI.UpdateInventory();
                merchantUI.UpdateSlot();
            }
        }
    }
}
