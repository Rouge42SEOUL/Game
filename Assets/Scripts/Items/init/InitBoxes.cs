using System.Collections.Generic;
using UnityEngine;

namespace Items.init
{
    public class InitBoxes
    {
        public static void InitInventoryBoxes(List<GameObject> inventoryPanels)
        {
            GameObject[] foundPanels = GameObject.FindGameObjectsWithTag("InventoryBoxes");
            if (foundPanels == null)
            { 
                Debug.LogError("is null");
            }
            // Add found GameObjects to the inventoryPanels list
            foreach (GameObject foundPanel in foundPanels)
            {
                inventoryPanels.Add(foundPanel.transform.GetChild(0).gameObject);
            }
        }
        
        public static void InitSlotBoxes(List<GameObject> slotPanels)
        {
            GameObject[] foundPanels = GameObject.FindGameObjectsWithTag("SlotBoxes");
            // Add found GameObjects to the inventoryPanels list
            foreach (GameObject foundPanel in foundPanels)
            {
                slotPanels.Add(foundPanel.transform.GetChild(0).gameObject);
            }
        }
    }
}