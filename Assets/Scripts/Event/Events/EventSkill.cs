using UnityEngine;

public class EventSkill :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Skill;
    }

    public override void BuildUI()
    {
    }
    
    public void Skill()
    {
        Debug.Log("스킬");
    }

}

