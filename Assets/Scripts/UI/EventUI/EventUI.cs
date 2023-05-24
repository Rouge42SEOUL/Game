using UnityEngine;

public abstract class EventUI : MonoBehaviour
{
    protected GameObject EventUIButton;
    private GameSceneManager _gameSceneManager;

    protected virtual void Start()
    {
        _gameSceneManager = GameSceneManager.Instance;
        EventUIButton = GameObject.Find("EventUIButton");
    }

    public void DisplayUI()
    {
        _gameSceneManager.InfoToJson.IsEventRunning = true;
        gameObject.SetActive(true);
        EventUIButton.gameObject.SetActive(false);
    }
    public void CloseUI()
    {
        _gameSceneManager.InfoToJson.IsEventRunning = true;
        EventUIButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
