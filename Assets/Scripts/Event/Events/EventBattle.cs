using SceneManagement;

public class EventBattle :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Battle;
    }

    public override void BuildUI()
    {
        Battle();
    }

    private void Battle() 
	{
		SceneManager.StartBattle();
	}
}

