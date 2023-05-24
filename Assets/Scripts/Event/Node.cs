using System;
using Unity.VisualScripting;
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
    private Sprite[] _sprites;

    private void Awake()
    {
        _sprites = Resources.LoadAll<Sprite>("Icon/map_sprite");
    }

    private void ChangeColor()
    {
        Debug.Log(_sprites.Length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // 대장장이0, 상인1, 엘리트2 , 상자3, 보스4, 배틀5
        if (eventType == EventType.BlackSmith)
            spriteRenderer.sprite = _sprites[0];
        else if (eventType == EventType.Merchant)
            spriteRenderer.sprite = _sprites[1];
        // else if (eventType == EventType.Elite)
        //     spriteRenderer.sprite = sprites[2];
        else if (eventType == EventType.Box)
            spriteRenderer.sprite = _sprites[3];
        else if (eventType == EventType.Boss)
            spriteRenderer.sprite = _sprites[4];
        else if (eventType == EventType.Battle)
            spriteRenderer.sprite = _sprites[5];
        else ;
    }

}
