using System;
using UnityEngine;

public enum EventType
{
    None,
    Battle,
    Skill,
    BlackSmith,
    Merchant,
    Box,
    Boss,
}

[Serializable]
public struct EventStruct
{
    public EventType type;
    public float probability;
};

namespace RougeEvent
{
    public abstract partial class Event
    {
        public EventType Type { get; protected set; }
        public abstract void BuildUI();
        public void UIControl()
        {
            Debug.Log("Event's UIControl");
            if (_isDisplay == false)
                DisplayEvent();
        }
    }

    public abstract partial class Event : MonoBehaviour // private
    {
        private bool _isDisplay = false;

        private void DisplayEvent()
        {
            gameObject.SetActive(true);
            _isDisplay = true;
        }

        private void CloseEvent()
        {
            gameObject.SetActive(false);
            _isDisplay = false;
        }
    }
}