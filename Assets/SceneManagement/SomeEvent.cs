using UnityEngine;

public abstract class SomeEvent : MonoBehaviour
{
    
    // UI를 상속받는 이벤트들이 각자 구성하기 위해 순수 가상 함수 사용
    public void DisplayEvent()
    {
        DisplayUI();
        BuildUI();
    }

    private void DisplayUI()
    {
        
    }
    private void CloseUI()
    {
        
    }
    protected abstract void BuildUI();
}
