using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items.ClickEvent.InventoryClick
{
    public class EventClickInventory : MonoBehaviour
    {
        public Inventory inventory;
        public List<Button> buttons;
    
        private void Start()
        {
            for (int i = 0; i < 16; i++)
            {
                int id = i;
                buttons[i].gameObject.AddComponent<RightClickable>().onClickRight.AddListener(delegate { InventoryClick(id); });
            }
        }
    
        private void InventoryClick(int id)
        {
            inventory.Save();
            if (inventory.EquipItemEvent(id))
            {
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
    }

    public class RightClickable : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onClickRight = new UnityEvent();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onClickRight.Invoke();
            }
        }
    }
}