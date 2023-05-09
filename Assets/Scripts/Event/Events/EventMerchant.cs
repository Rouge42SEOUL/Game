using UnityEngine;

public class EventMerchant :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Merchant;
    }

    public override void BuildUI()
    {
        Merchant();
    }
    
    public void Merchant()
    {
        Debug.Log("상인");
    }

}
