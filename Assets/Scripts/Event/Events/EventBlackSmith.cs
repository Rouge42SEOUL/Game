using System;
using UnityEngine;
public class EventBlackSmith : RougeEvent.Event
{
    private BlackSmithUI _blackSmithUI;
    private void Awake()
    {
        Type = EventType.BlackSmith;
    }

    private void Start()
    {
        _blackSmithUI = FindObjectOfType<BlackSmithUI>();
    }

    public override void BuildUI()
    {
        _blackSmithUI.DisplayUI();
        _blackSmithUI.OptionRandomSetting();
        _blackSmithUI.transform.localPosition = Vector3.zero;
    }
}
