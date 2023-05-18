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
	[JsonProperty]
    public int Gold;
	[JsonProperty]
    public bool IsEventRunning;

    public void SaveInfo(Node[] nodes, Node currentNode)
    {
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
    public InfoToJson InfoToJson;
    [SerializeField] private int firstGold;
    [SerializeField] private PlayerPawn playerPawn;
    [SerializeField] private GameObject eventUI;
    [SerializeField] private string jsonFileName = "/test.json";
	public int Gold
	{
	    get => InfoToJson.Gold;
	    private set
	    {
            InfoToJson.Gold = value;
			_goldUI.Gold = InfoToJson.Gold;
		}
	}
	public void MovePlayer(Node node)
    {
        OptionUIControl();
        _currentNode = node;
        playerPawn.MoveToNode(node);
        SaveCurrentInfo();
    }
	public void SaveCurrentInfo()
    {
        InfoToJson.Map = _stageManager.MapNum;
		InfoToJson.SaveInfo(_stageManager.Nodes, _currentNode);
		JsonConverter.Save(InfoToJson, Application.dataPath + jsonFileName);
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
    private Node _currentNode;
	private GoldUI _goldUI;
    private bool _isDisplayUI;
    private bool _isFirstStart;

    private void Start()
    {
		// Init
        _eventManager = EventManager.Instance;
        _stageManager = StageManager.Instance;
		_goldUI = FindObjectOfType<GoldUI>();
		// Load Prev Data
        _isFirstStart = !JsonConverter.Load(out InfoToJson, Application.dataPath + jsonFileName);
        if (_isFirstStart)
        {
            _stageManager.RandomInit();
            Gold = firstGold;
        }
        else
        {
            _stageManager.PrevInit(InfoToJson);
			Gold = InfoToJson.Gold;
		}
		_goldUI.Gold = Gold;
        _currentNode = _stageManager.Nodes[InfoToJson.PlayerCurrentNode];
        playerPawn.MoveToNode(_currentNode);
        SaveCurrentInfo();
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
