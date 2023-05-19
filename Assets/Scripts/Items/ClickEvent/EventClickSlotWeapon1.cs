using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent
{
    public class EventClickSlotWeapon1 : MonoBehaviour, IPointerClickHandler
    {
        public Inventory inventory;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventory.ReleasingWeaponItem1();
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
    }
}
