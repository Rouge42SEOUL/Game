using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameManager obj = FindObjectOfType<GameManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    GameManager newObj = new GameObject().AddComponent<GameManager>();
                    _instance = newObj;
                }
            }
            return _instance;
        }
    }
    private EventManager _eventManager;
    private StageManager _stageManager;
    private bool _isDisplayToEventUI = false;
    private bool _isFirstStart;
    private JsonSaveLoder _jsonSaveLoder;
    
    public InfoToJson InfoToJson;
    [SerializeField] public PlayerPawn playerPawn;
    [SerializeField] private GameObject eventUI;
    
    public Node currentNode;


    private void Awake()
    {
        GameManager[] obj = FindObjectsOfType<GameManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        _jsonSaveLoder = gameObject.AddComponent<JsonSaveLoder>();
        _eventManager = FindObjectOfType<EventManager>();
        _stageManager = FindObjectOfType<StageManager>(); // 싱글톤
        
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
