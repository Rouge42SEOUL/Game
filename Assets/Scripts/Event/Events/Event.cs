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

        public virtual void BuildUI()
        {
            GameSceneManager.InfoToJson.IsEventRunning = true;
            GameSceneManager.SaveCurrentInfo();
        }
    }

    public abstract partial class Event : MonoBehaviour // private
    {
        protected GameSceneManager GameSceneManager;

        private void Start()
        {
            GameSceneManager = GameSceneManager.Instance;
        }
    }
}