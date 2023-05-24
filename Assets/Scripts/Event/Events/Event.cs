using System;
using Managers.DataManager;
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

        public virtual void BuildUI()
        {
            DataManager.Instance.SetRunningEvent(true);
            DataManager.Instance.SaveData();
        }
    }

    public abstract partial class Event : MonoBehaviour // private
    {
    }
}