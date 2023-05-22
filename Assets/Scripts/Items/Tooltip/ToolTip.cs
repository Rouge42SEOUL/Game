using System.Collections;
using Items.ScriptableObjectSource;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items.Tooltip
{
    public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject toolTipPanel;
        public Text toolTipText;

        public SlotType slotType; // 슬롯의 유형을 나타내는 변수
        public int itemIndex; // 인벤토리 아이템을 참조하는 경우에만 사용

        private Coroutine _hoverCoroutine;

        private void Start()
        {
            toolTipPanel.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverCoroutine = StartCoroutine(HoverCoroutine());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverCoroutine != null)
            {
                StopCoroutine(_hoverCoroutine);
                _hoverCoroutine = null;
            }

            toolTipPanel.SetActive(false);
        }

        private IEnumerator HoverCoroutine()
        {
            yield return new WaitForSeconds(0.8f);

            // Set the position of the tooltip panel to the mouse position
            toolTipPanel.transform.position = Input.mousePosition + new Vector3(10, -10, 0);
            toolTipPanel.SetActive(true);

            // Set the text of the tooltip
            Item item = null;

            // 슬롯의 유형에 따라 참조하는 아이템을 변경
            switch (slotType)
            {
                case SlotType.Inventory:
                    item = Inventory.Instance.inventoryItems[itemIndex];
                    break;
                case SlotType.Weapon0:
                    item = Slot.Instance.slotWeapon[0];
                    break;
                case SlotType.Weapon1:
                    item = Slot.Instance.slotWeapon[1];
                    break;
                case SlotType.Armor:
                    item = Slot.Instance.slotArmor;
                    break;
                case SlotType.Necklace:
                    item = Slot.Instance.slotNecklace;
                    break;
                case SlotType.Ring0:
                    item = Slot.Instance.slotRing[0];
                    break;
                case SlotType.Ring1:
                    item = Slot.Instance.slotRing[1];
                    break;
            }

            if (item != null)
            {
                if(item is Weapon weapon)
                {
                    toolTipText.text = weapon.ItemDescription();
                }
                else if(item is Armor armor)
                {
                    toolTipText.text = armor.ItemDescription();
                }
                else if(item is Necklace necklace)
                {
                    toolTipText.text = necklace.ItemDescription();
                }
                else if(item is Ring ring)
                {
                    toolTipText.text = ring.ItemDescription();
                }
                else
                {
                    toolTipText.text = "";
                }
            }
            else
            {
                toolTipText.text = "";
            }
        }
    }
}
