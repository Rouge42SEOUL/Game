using Items;
using UnityEngine;

public class EventMerchant :  RougeEvent.Event
{
    private MerchantUI _merchantUI;
    private Inventory _inventory;
    private void Awake()
    {
        Type = EventType.Merchant;
        _merchantUI = FindObjectOfType<MerchantUI>();
    }

    public override void BuildUI()
    {
        _merchantUI.DisplayUI();
        _merchantUI.OptionRandomSetting();
        _merchantUI.transform.localPosition = Vector3.zero;
    }
}
