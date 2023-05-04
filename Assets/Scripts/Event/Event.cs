using System;
using UnityEngine;

public enum EventType
{
    Battle,
    Skill,
    BlackSmith,
    Merchent,
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
            if (_isDisplay == false)
                DisplayEvent();
            else
                CloseEvent();
        }

        protected abstract void BuildUI();

        private void Start()
        {
            CloseEvent();
        }

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