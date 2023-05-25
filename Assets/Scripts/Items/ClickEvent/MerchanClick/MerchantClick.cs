using UnityEngine;
using UnityEngine.UI;

namespace Items.ClickEvent.InventoryClick
{
    public class MerchantClick : MonoBehaviour
    {
        public Button button;

        private void Start()
        {
            // Fetch the MerchantOption component from the same GameObject this script is attached to
            MerchantOption merchantOption = GetComponent<MerchantOption>();
            
            // Ensure MerchantOption was found before attempting to use it
            if(merchantOption != null)
            {
                // Add the RightClickable component to the button and assign the buy method of MerchantOption as the action for right-clicks
                button.gameObject.AddComponent<RightClickable>().onClickRight.AddListener(merchantOption.Buy);
            }
            else
            {
                Debug.LogError("MerchantOption component not found on this GameObject", this);
            }
        }
    }
}