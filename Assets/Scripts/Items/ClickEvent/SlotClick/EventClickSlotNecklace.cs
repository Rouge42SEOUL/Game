using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent.SlotClick
{
    public class EventClickSlotNecklace : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;

        private void Start()
        {
            _inventory = Inventory.Instance;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_inventory.slot.slotNecklace == null)
                    return;
                _inventory.ReleasingNecklaceItem();
                _inventory.UpdateInventory();
                _inventory.UpdateSlot();
            }
        }
    }
}
