using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EventManager _eventManager;
    private StageManager _stageManager;
    private GameObject _playerPawn;
    private Node _currentNode;
    private bool _isDisplayToeventUI = false;
    [SerializeField] private GameObject eventUI;

    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        _stageManager = FindObjectOfType<StageManager>();
         _currentNode = _stageManager._nodes[0];
        // _eventManager.MovePlayerPawn(_currentNode);
        CloseEvent();
    }
    
    public void UIControl()
    {
        if (_isDisplayToeventUI == false)
            DisplayEvent();
        else
            CloseEvent();
    }
    
    private void DisplayEvent()
    {
        eventUI.gameObject.SetActive(true);
        _isDisplayToeventUI = true;
    }

    private void CloseEvent()
    {
        eventUI.gameObject.SetActive(false);
        _isDisplayToeventUI = false;
    }
    
}
