using System.Collections.Generic;
using Items.ScriptableObjectSource;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class InventoryBoxPanel : MonoBehaviour
    {
        public Image itemIconImage;

        public void UpdateItem(Item item)
        {
            if (item == null)
            {
                itemIconImage.color = new Color(0, 0, 0, 0);
            }
            else
            {
                itemIconImage.color = Color.white;
            }
            itemIconImage.sprite = item != null ? item.icon : null;
        }
    }
}