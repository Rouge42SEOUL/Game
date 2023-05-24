using System.Collections.Generic;
using UnityEngine;

namespace Items.init
{
    public class InitBoxes
    {
        public void InitInventoryBoxes(List<GameObject> inventoryPanels, string tag)
        {
            GameObject[] foundPanels = GameObject.FindGameObjectsWithTag(tag);
            if (foundPanels == null)
            {
                Debug.LogError("None of Inventory Box Tage");
            }
            // Add found GameObjects to the inventoryPanels list
            foreach (GameObject foundPanel in foundPanels)
            {
                inventoryPanels.Add(foundPanel.transform.GetChild(0).gameObject);
            }
        }
        
        public void InitSlotBoxes(List<GameObject> slotPanels, string tag)
        {
            GameObject[] foundPanels = GameObject.FindGameObjectsWithTag(tag);
            if (foundPanels == null)
            {
                Debug.LogError("None of Slot Box Tage");
            }
            // Add found GameObjects to the inventoryPanels list
            foreach (GameObject foundPanel in foundPanels)
            {
                slotPanels.Add(foundPanel.transform.GetChild(0).gameObject);
            }
        }
    }
}