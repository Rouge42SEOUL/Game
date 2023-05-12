using System;
using UnityEngine;
using Event = RougeEvent.Event;

public partial class Node // public
{
    public Node[] nextNode; // 하드코딩
    public Event @event;
    public EventType eventType;
    public int positionX;
    public int positionY; // Round 계층
    
    public void EventSetting()
    {
        @event = eventType switch
        {
            EventType.Battle => gameObject.AddComponent<EventBattle>(),
            EventType.BlackSmith => gameObject.AddComponent<EventBlackSmith>(),
            EventType.Skill => gameObject.AddComponent<EventSkill>(),
            EventType.Boss => gameObject.AddComponent<EventBoss>(),
            EventType.Merchant => gameObject.AddComponent<EventMerchant>(),
            EventType.Box => gameObject.AddComponent<EventBox>(),
            _ => @event
        };
        ChangeColor();
    }
}
[Serializable]
public partial class Node : MonoBehaviour // private
{
    private readonly Color[] _colors = new Color[7]
        {Color.white, Color.yellow, Color.cyan, Color.blue, Color.green, Color.gray, Color.black};
    
    private void ChangeColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _colors[(int)eventType];
    }

}
