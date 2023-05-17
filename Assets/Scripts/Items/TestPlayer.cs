using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Inventory inventory;
    
    void Start()
    {
        inventory.GetArmor(0);
    }
}
