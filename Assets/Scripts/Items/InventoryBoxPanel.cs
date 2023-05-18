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
            itemIconImage.sprite = item != null ? item.icon : null;
        }
    }
}