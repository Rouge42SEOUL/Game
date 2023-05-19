using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent
{
    public class EventClickSlotRing0 : MonoBehaviour, IPointerClickHandler
    {
        public Inventory inventory;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventory.ReleasingRingItem0();
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
    }
}
