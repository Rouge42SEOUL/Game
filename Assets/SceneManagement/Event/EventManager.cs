using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    public RougeEvent.Event[] someEvents;
    private Dictionary<string, RougeEvent.Event> _events = new Dictionary<string, RougeEvent.Event>();

    private void Start()
    {
        foreach (RougeEvent.Event someEvent in someEvents)
        {
            _events.Add(someEvent.Name, someEvent);
        }
    }

    public void EventAction(string eventName)
    {
        RougeEvent.Event value;
        if (_events.TryGetValue(eventName, out value) == false)
            Debug.Log("찾고자 하는 이벤트가 없습니다");
        else
            value.UIControl();
    }
}