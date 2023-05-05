using UnityEngine;

public class EventBox :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Box;
    }

    protected override void BuildUI()
    {
    }
    
    public void Box()
    {
        Debug.Log("박스");
    }
}

