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
        public InventoryClickEventType inventoryClickEventType;
        public GameSceneManager gameSceneManager;
        public MerchantUI merchantUI;

        private void Start()
        {
            for (int i = 0; i < 16; i++)
            {
                int id = i;
                if (inventoryClickEventType	== InventoryClickEventType.NormalEvent)
                    buttons[i].gameObject.AddComponent<RightClickable>().onClickRight.AddListener(delegate { NormalClick(id); });
                else if (inventoryClickEventType == InventoryClickEventType.MerchantEvent)
                    buttons[i].gameObject.AddComponent<RightClickable>().onClickRight.AddListener(delegate { MerchantClick(id); });

            }
        }
    
        private void NormalClick(int id)
        {
            if (inventory.EquipItemEvent(id))
            {
                inventory.UpdateInventory();
                inventory.UpdateSlot();
            }
        }
        
        private void MerchantClick(int id)
        {
            if (inventory.inventoryItems[id])
            {
                gameSceneManager.Gold += (int)(inventory.inventoryItems[id].gold * 0.7);
                inventory.inventoryItems[id] = null;
                merchantUI.UpdateInventory();
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