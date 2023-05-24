using UnityEngine;

public class EventBox :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Box;
    }

    public override void BuildUI()
    {
        base.BuildUI();
        Box();
    }
    
    public void Box()
    {
        Debug.Log("박스");
    }
}

