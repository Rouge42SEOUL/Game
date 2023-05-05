using UnityEngine;

public class EventBoss :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Boss;
    }

    protected override void BuildUI()
    {
    }
    
    public void Boss()
    {
        Debug.Log("보스");
    }

}
