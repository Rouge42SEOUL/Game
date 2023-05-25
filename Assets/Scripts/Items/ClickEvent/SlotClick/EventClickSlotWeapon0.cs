using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent.SlotClick
{
    public class EventClickSlotWeapon0 : MonoBehaviour, IPointerClickHandler
    {
        private Inventory _inventory;

        private void Start()
        {
            _inventory = Inventory.Instance;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _inventory.Save();
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_inventory.slot.slotWeapon[0] == null)
                    return;
                _inventory.ReleasingWeaponItem0();
                _inventory.UpdateInventory();
                _inventory.UpdateSlot();
            }
        }
    }
}
