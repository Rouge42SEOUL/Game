using UnityEngine;

public class EventBoss :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Boss;
    }

    public override void BuildUI()
    {
        base.BuildUI();
        Boss();
    }
    
    public void Boss()
    {
        Debug.Log("보스");
		JsonConverter.DeleteJson("/Json/test.json");
    }

}
