using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


[JsonObject(MemberSerialization.OptIn)] //지정한 데이터만 변환하게 하는 설정
public class InfoToJson
{
    [JsonProperty]
    public int Map;
    [JsonProperty] 
    public Dictionary<int, EventType> Events;
    [JsonProperty]
    public int PlayerCurrentNode;

    public void SaveInfo(int mapNum, Node[] nodes, Node currentNode)
    {
        Map = mapNum;
        Events = new Dictionary<int, EventType>();
        for (int i = 0; i < nodes.Length; i++)
        {
            Events.Add(i, nodes[i].eventType);
            if (nodes[i] == currentNode)
            {
                PlayerCurrentNode = i;
            }
        }
    }
}

public class GameManager : MonoBehaviour
{
    private EventManager _eventManager;
    private StageManager _stageManager;
    private bool _isDisplayToEventUI = false;
    private bool _isFirstStart;
    private JsonSaveLoder _jsonSaveLoder;
    
    public InfoToJson InfoToJson;
    [SerializeField] public PlayerPawn playerPawn;
    [SerializeField] private GameObject eventUI;
    
    public Node currentNode;

    private void Start()
    {
        _jsonSaveLoder = gameObject.AddComponent<JsonSaveLoder>();
        _eventManager = FindObjectOfType<EventManager>();
        _stageManager = FindObjectOfType<StageManager>();
        
        _isFirstStart = _jsonSaveLoder.Load(out InfoToJson);
        if (_isFirstStart)
        {
            // 첫시작시 랜덤으로 초기화    
            _stageManager.RandomInit();
        }
        else
        {
            // infoToJson 데이터를 StageManager에 동기화 맵, 노드들
            _stageManager.PrevInit(InfoToJson);
        }
        currentNode = _stageManager.Nodes[InfoToJson.PlayerCurrentNode];
        playerPawn.MoveToNode(currentNode);
        CloseEvent();
        InfoToJson.SaveInfo(_stageManager.MapNum, _stageManager.Nodes, currentNode);
        _jsonSaveLoder.Save(InfoToJson);
    }

    public void SaveCurrentInfo()
    {
        InfoToJson.SaveInfo(_stageManager.MapNum, _stageManager.Nodes, currentNode);
        _jsonSaveLoder.Save(InfoToJson);
    }
    
    public void UIControl()
    {
        Debug.Log("gameManager's UIControl");
        if (_isDisplayToEventUI == false)
            DisplayEvent();
        else
            CloseEvent();
    }
    
    private void DisplayEvent()
    {
        eventUI.gameObject.SetActive(true);
        _isDisplayToEventUI = true;
    }

    private void CloseEvent()
    {
        eventUI.gameObject.SetActive(false);
        _isDisplayToEventUI = false;
    }
    
}
