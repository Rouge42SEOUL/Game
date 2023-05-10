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

public partial class GameManager // public
{
    [SerializeField] private PlayerPawn playerPawn;
    [SerializeField] private GameObject eventUI;
    [SerializeField] private string jsonFileName = "/test.json";

    public void MovePlayer(Node node)
    {
        OptionUIControl();
        playerPawn.MoveToNode(node);
        _currentNode = node;
        SaveCurrentInfo();
    }
    
    public void OptionUIControl()
    {
        if (_isDisplayUI == false)
        {
            eventUI.gameObject.SetActive(true);
            _isDisplayUI = true;
        }
        else
        {
            eventUI.gameObject.SetActive(false);
            _isDisplayUI = false;
        }
    }
}

public partial class GameManager : MonoBehaviour // private
{
    private EventManager _eventManager;
    private StageManager _stageManager;
    private bool _isDisplayUI;
    private bool _isFirstStart;
    private InfoToJson _infoToJson;
    private Node _currentNode;

    private void Start()
    {
        _eventManager = EventManager.Instance;
        _stageManager = StageManager.Instance;
        _isFirstStart = !JsonConverter.Load(out _infoToJson, Application.dataPath + jsonFileName);
        if (_isFirstStart)
        {
            _stageManager.RandomInit();
        }
        else
        {
            _stageManager.PrevInit(_infoToJson);
        }
        _currentNode = _stageManager.Nodes[_infoToJson.PlayerCurrentNode];
        playerPawn.MoveToNode(_currentNode);
        SaveCurrentInfo();
    }

    private void SaveCurrentInfo()
    {
        _infoToJson.SaveInfo(_stageManager.MapNum, _stageManager.Nodes, _currentNode);
        JsonConverter.Save(_infoToJson, Application.dataPath + jsonFileName);
    }

}
public partial class GameManager // singleton
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
    private void Awake()
    {
        GameManager[] obj = FindObjectsOfType<GameManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
