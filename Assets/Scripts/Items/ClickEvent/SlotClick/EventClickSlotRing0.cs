﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent.SlotClick
{
    public class EventClickSlotRing0 : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;
        public MerchantUI merchantUI;

        private void Start()
        {
            _inventory = Inventory.Instance;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_inventory.slot.slotRing[0] == null)
                {
                    _inventory.Save();
                    return;
                }
                _inventory.ReleasingRingItem0();
                _inventory.UpdateInventory();
                _inventory.UpdateSlot();
                if (merchantUI == null)
                {
                    _inventory.Save();
                    return;
                }
                merchantUI.UpdateInventory();
                merchantUI.UpdateSlot();
            }
        }
    }
}
