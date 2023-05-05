using UnityEngine;

public class EventMerchant :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Merchant;
    }

    protected override void BuildUI()
    {
    }
    
    public void Merchant()
    {
        Debug.Log("상인");
    }

}
