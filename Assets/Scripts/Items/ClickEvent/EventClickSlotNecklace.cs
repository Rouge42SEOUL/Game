using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent
{
    public class EventClickSlotNecklace : MonoBehaviour, IPointerClickHandler
    {
        public Inventory inventory;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventory.ReleasingNecklaceItem();
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
    }
}
