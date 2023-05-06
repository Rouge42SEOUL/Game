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
    
    public void Battle()
    {
        SceneManager sceneManager = gameObject.AddComponent<SceneManager>();
        sceneManager.StartBattle();
    }
}

