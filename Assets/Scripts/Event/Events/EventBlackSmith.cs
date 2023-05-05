using UnityEngine;
public class EventBlackSmith :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.BlackSmith;
    }

    protected override void BuildUI()
    {
    }
    
    // 카드(버튼) 선택시 호출될 함수들 
    public void Upgrade()
    {
        Debug.Log("강화");
    }
    public void Dismantle()
    {
        Debug.Log("추출");
    }
}
