using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


[JsonObject(MemberSerialization.OptIn)]
public class InfoToJson
{
    [JsonProperty]
    public int Map;
    [JsonProperty]
    public Dictionary<int, EventType>[] Events;
    [JsonProperty]
    public int PlayerCurrentPosition;
}

public class GameManager : MonoBehaviour
{
    private EventManager _eventManager;
    private StageManager _stageManager;
    private bool _isDisplayToeventUI = false;
    private bool _isFirstStart;
    private InfoToJson _infoToJson;
    
    [SerializeField] private PlayerPawn playerPawn;
    [SerializeField] private GameObject eventUI;
    
    public Node currentNode;

    private void Start()
    {
        JsonSaveLoder jsonSaveLoder = gameObject.AddComponent<JsonSaveLoder>();

        _isFirstStart = jsonSaveLoder.Load(out _infoToJson);
        _eventManager = FindObjectOfType<EventManager>();
        _stageManager = FindObjectOfType<StageManager>();
        if (_isFirstStart)
            currentNode = _stageManager._nodes[0];
        playerPawn.MoveToNode(currentNode);
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
