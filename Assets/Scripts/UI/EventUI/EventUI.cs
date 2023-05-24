using UnityEngine;

public abstract class EventUI : MonoBehaviour
{
    protected GameObject EventUIButton;
    private MapDataManager _mapDataManager;

    protected virtual void Start()
    {
        _mapDataManager = MapDataManager.Instance;
        EventUIButton = GameObject.Find("EventUIButton");
    }

    public void DisplayUI()
    {
        _mapDataManager.SetEventRunning();
        gameObject.SetActive(true);
        EventUIButton.gameObject.SetActive(false);
    }
    public void CloseUI()
    {
        _mapDataManager.SetEventRunning();
        EventUIButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
