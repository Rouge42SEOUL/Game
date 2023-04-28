using UnityEngine;

public class EventBlackSmith : SomeEvent
{
    private void Awake()
    {
        Name = "BlackSmith";
    }
    protected override void BuildUI()
    {
    }
    public void Upgrade()
    {
        Debug.Log("강화");
    }
    public void Dismantle()
    {
        Debug.Log("추출");
    }
}
