using System;
using UnityEngine;
using Event = RougeEvent.Event;

[Serializable]
public class Node : MonoBehaviour
{
    public Node[] nextNode;
    public Event _event;
    public int positionX;
    public int positionY; // Round 계층
    public EventType eventType;
    
    private readonly Color[] _colors = new Color[7]
        {Color.white, Color.yellow, Color.cyan, Color.blue, Color.green, Color.gray, Color.black};

    public void EventSetting()
    {
        if (eventType == EventType.Battle)
            _event = gameObject.AddComponent<EventBattle>();
        else if (eventType == EventType.BlackSmith)
            _event = gameObject.AddComponent<EventBlackSmith>();
        else if (eventType == EventType.Skill)
            _event = gameObject.AddComponent<EventSkill>();
        else if (eventType == EventType.Boss)
            _event = gameObject.AddComponent<EventBoss>();
        else if (eventType == EventType.Merchant)
            _event = gameObject.AddComponent<EventMerchant>();
        else if (eventType == EventType.Box)
            _event = gameObject.AddComponent<EventBox>();
    }

    public void ChangeColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _colors[(int)eventType];
    }

}
