using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.ClickEvent
{
    public class EventClickSlotArmor : MonoBehaviour, IPointerClickHandler
    {
        public Inventory inventory;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventory.ReleasingArmorItem();
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
    }
}
