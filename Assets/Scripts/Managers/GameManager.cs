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
	[SerializeField] private int _gold;
	public int Gold
	{
	    get => _gold;
        set
	    {
			_gold = value;
			_goldUI.Gold = _gold;
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
		_infoToJson.SaveInfo(_stageManager.MapNum, _stageManager.Nodes, _currentNode);
		_infoToJson.Gold = _gold;
		JsonConverter.Save(_infoToJson, Application.dataPath + jsonFileName);
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
    private InfoToJson _infoToJson;
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
        _isFirstStart = !JsonConverter.Load(out _infoToJson, Application.dataPath + jsonFileName);
        if (_isFirstStart)
        {
            _stageManager.RandomInit();
        }
        else
        {
            _stageManager.PrevInit(_infoToJson);
			Gold = _infoToJson.Gold;
		}
		_goldUI.Gold = _gold;
        _currentNode = _stageManager.Nodes[_infoToJson.PlayerCurrentNode];
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
