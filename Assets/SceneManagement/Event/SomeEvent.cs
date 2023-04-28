using System;
using UnityEngine;

public abstract class SomeEvent : MonoBehaviour
{
    private bool _isDisplay = false;
    public string Name { get; protected set; }

    private void Awake()
    {
        CloseUI();
    }

    public void UIControl()
    {
        if (!_isDisplay)
            DisplayUI();
        else
            CloseUI();
        
    }
    // UI를 상속받는 이벤트들이 각자 구성하기 위해 순수 가상 함수 사용
    public void DisplayEvent()
    {
        DisplayUI();
        BuildUI();
    }

    private void DisplayUI()
    {
        gameObject.SetActive(true);
        _isDisplay = true;
    }
    private void CloseUI()
    {
        gameObject.SetActive(false);
        _isDisplay = false;
    }
    protected abstract void BuildUI();
}
