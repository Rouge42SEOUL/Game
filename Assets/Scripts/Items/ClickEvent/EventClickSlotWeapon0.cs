using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent
{
    public class EventClickSlotWeapon0 : MonoBehaviour, IPointerClickHandler
    {
        public Inventory inventory;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventory.ReleasingWeaponItem0();
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
    }
}
