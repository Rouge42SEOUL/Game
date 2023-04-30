using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public SomeEvent[] someEvents;
    private Dictionary<string, SomeEvent> _events = new Dictionary<string, SomeEvent>();

    private void Start()
    {
        foreach (SomeEvent someEvent in someEvents)
        {
            _events.Add(someEvent.Name, someEvent);
        }
    }

    public void EventAction(string eventName)
    {
        SomeEvent value;
        if (_events.TryGetValue(eventName, out value) == false)
            Debug.Log("찾고자 하는 이벤트가 없습니다");
        else
            value.UIControl();
    }
}