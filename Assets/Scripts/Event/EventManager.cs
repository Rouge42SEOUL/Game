using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public RougeEvent.Event[] events;
    private Dictionary<EventType, RougeEvent.Event> _events 
        = new Dictionary<EventType, RougeEvent.Event>();
    private void Start()
    {
        foreach (RougeEvent.Event tmpEvent in events)
        {
            _events.Add(tmpEvent.Type, tmpEvent);
        }
    }

    public void EventAction(EventType eventType)
    {
        if (_events.TryGetValue(eventType, out RougeEvent.Event value) == false)
        {
            Debug.Log("찾고자 하는 이벤트가 없습니다");
        }
        else
        {
            value.UIControl();
        }
    }

}