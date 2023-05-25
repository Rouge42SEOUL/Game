using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent.SlotClick
{
    public class EventClickSlotArmor : MonoBehaviour, IPointerClickHandler
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
                if (_inventory.slot.slotArmor == null)
                    return;
                _inventory.ReleasingArmorItem();
                _inventory.UpdateInventory();
                _inventory.UpdateSlot();
                if (merchantUI == null) return;
                merchantUI.UpdateInventory();
                merchantUI.UpdateSlot();
            }
        }
    }
}
