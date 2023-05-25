using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items.ClickEvent.SlotClick
{
    public class EventClickSlotTotal : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;
        public SlotType slotType;
        public Button button;
        public MerchantUI merchantUI;

        private void Start()
        {
            _inventory = Inventory.Instance;

            // Add an EventTrigger component to the Button's GameObject
            EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

            // Create a new Entry for our Right Click event
            EventTrigger.Entry rightClickEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            // Add a callback function to the Right Click entry
            rightClickEntry.callback.AddListener((eventData) => OnPointerClick((PointerEventData)eventData));

            // Add the Right Click entry to the EventTrigger on the Button's GameObject
            eventTrigger.triggers.Add(rightClickEntry);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                switch (slotType)
                {
                    case SlotType.Weapon0:
                        if (_inventory.slot.slotWeapon[0] != null)
                        {
                            _inventory.ReleasingWeaponItem0();
                        }
                        break;
                    case SlotType.Weapon1:
                        if (_inventory.slot.slotWeapon[1] != null)
                        {
                            _inventory.ReleasingWeaponItem1();
                        }
                        break;
                    case SlotType.Armor:
                        if (_inventory.slot.slotArmor != null)
                        {
                            _inventory.ReleasingArmorItem();
                        }
                        break;
                    case SlotType.Necklace:
                        if (_inventory.slot.slotNecklace != null)
                        {
                            _inventory.ReleasingNecklaceItem();
                        }
                        break;
                    case SlotType.Ring0:
                        if (_inventory.slot.slotRing[0] != null)
                        {
                            _inventory.ReleasingRingItem0();
                        }
                        break;
                    case SlotType.Ring1:
                        if (_inventory.slot.slotRing[1] != null)
                        {
                            _inventory.ReleasingRingItem1();
                        }
                        break;
                    case SlotType.Inventory:
                        Debug.LogWarning("You should not be able to click the Inventory slot.");
                        break;
                }
                _inventory.UpdateInventory();
                _inventory.UpdateSlot();
                if (merchantUI != null)
                {
                    merchantUI.UpdateInventory();
                    merchantUI.UpdateSlot();
                }
            }
        }
    }
}