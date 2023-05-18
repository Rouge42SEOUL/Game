using UnityEngine;

public abstract class EventUI : MonoBehaviour
{
    protected GameObject _eventUIButton;
    private GameManager _gameManager;

    protected virtual void Start()
    {
        _gameManager = GameManager.Instance;
        _eventUIButton = GameObject.Find("EventUIButton");
    }

    public void DisplayUI()
    {
        _gameManager.InfoToJson.IsEventRunning = true;
        gameObject.SetActive(true);
        _eventUIButton.gameObject.SetActive(false);
    }
    public void CloseUI()
    {
        _gameManager.InfoToJson.IsEventRunning = true;
        _eventUIButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
