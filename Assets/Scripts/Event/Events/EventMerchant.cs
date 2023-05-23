using UnityEngine;

public class EventMerchant :  RougeEvent.Event
{
    private MerchantUI _merchantUI;
    private void Awake()
    {
        Type = EventType.Merchant;
    }
    private void Start()
    {
        _merchantUI = FindObjectOfType<MerchantUI>();
    }

    public override void BuildUI()
    {
        _merchantUI.DisplayUI();
        _merchantUI.OptionRandomSetting();
        _merchantUI.transform.localPosition = Vector3.zero;
    }
}
