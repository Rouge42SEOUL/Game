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
    public EventType Type;
    public float Probability;
};

namespace RougeEvent
{
    public abstract class Event : MonoBehaviour // namespace 
    {
        private bool _isDisplay = false;

        public EventType Type { get; protected set; }

        public void UIControl()
        {
            Debug.Log("Event's UIControl");
            if (_isDisplay == false)
                DisplayEvent();
        }

        public abstract void BuildUI();

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