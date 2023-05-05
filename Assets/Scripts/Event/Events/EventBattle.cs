using SceneManagement;

public class EventBattle :  RougeEvent.Event
{
    private void Awake()
    {
        Type = EventType.Battle;
    }

    protected override void BuildUI()
    {
    }
    
    public void Battle()
    {
        SceneManager sceneManager = gameObject.AddComponent<SceneManager>();
        sceneManager.StartBattle();
    }
}

