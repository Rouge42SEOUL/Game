using UnityEngine;

public abstract class EventUI : MonoBehaviour
{
    protected GameObject EventUIButton;
    private GameManager _gameManager;

    protected virtual void Start()
    {
        _gameManager = GameManager.Instance;
        EventUIButton = GameObject.Find("EventUIButton");
    }

    public void DisplayUI()
    {
        _gameManager.InfoToJson.IsEventRunning = true;
        gameObject.SetActive(true);
        EventUIButton.gameObject.SetActive(false);
    }
    public void CloseUI()
    {
        _gameManager.InfoToJson.IsEventRunning = true;
        EventUIButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
