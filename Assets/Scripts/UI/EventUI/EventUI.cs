using UnityEngine;

public abstract class EventUI : MonoBehaviour
{
    protected GameObject _eventUIButton;

    protected virtual void Start()
    {
        _eventUIButton = GameObject.Find("EventUIButton");
    }

    public void DisplayUI()
    {
        gameObject.SetActive(true);
        _eventUIButton.gameObject.SetActive(false);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
        _eventUIButton.gameObject.SetActive(true);
    }
}
