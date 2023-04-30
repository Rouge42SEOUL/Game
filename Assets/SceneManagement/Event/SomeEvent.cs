using UnityEngine;

public abstract class SomeEvent : MonoBehaviour
{
    private bool _isDisplay = false;
    public string Name { get; protected set; }

    public void UIControl()
    {
        if (_isDisplay == false)
            DisplayEvent();
        else
            CloseEvent();
    }
    
    protected abstract void BuildUI();

    private void Start()
    {
        CloseEvent();
    }

    private void DisplayEvent()
    {
        gameObject.SetActive(true);
        _isDisplay = true;
    }
    private void CloseEvent()
    {
        gameObject.SetActive(false);
        _isDisplay = false;
    }
}
