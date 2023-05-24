using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameScene;
using Newtonsoft.Json;
using TMPro;
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

public partial class GameSceneManager // public
{
    public InfoToJson InfoToJson;
    [SerializeField] private int firstGold;
    [SerializeField] private PlayerPawn playerPawn;
    [SerializeField] private string jsonFileName = "Json/GameManager.json";
	public int Gold
	{
	    get => InfoToJson.Gold;
        set
	    {
            InfoToJson.Gold = value;
			_uiManager.goldUI.Gold = InfoToJson.Gold;
		}
	}
	public void MovePlayer(Node node)
    {
		_uiManager.OptionUIControl();
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
}

public partial class GameSceneManager : MonoBehaviour // private
{
    private EventManager _eventManager;
    private StageManager _stageManager;
	private UIManager _uiManager;
    private Node _currentNode;
    private bool _isFirstStart;

    private void Start()
    {
		// Init
        _eventManager = EventManager.Instance;
        _stageManager = StageManager.Instance;
		_uiManager = UIManager.Instance;
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
		_uiManager.goldUI.Gold = Gold;
        _currentNode = _stageManager.Nodes[InfoToJson.PlayerCurrentNode];
        playerPawn.MoveToNode(_currentNode);
        SaveCurrentInfo();
        CheckPrevEventRun(_currentNode);
    }
    async void CheckPrevEventRun(Node node) // 객체들간의 초기화순서때문에 Null Reference Exception이.. 호출돼서
    {
        if (InfoToJson.IsEventRunning == true)
        {
            await Task.Delay(10);
            _eventManager.EventAction(node);
        }
    }
}
public partial class GameSceneManager // singleton
{
    private static GameSceneManager _instance;

    public static GameSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameSceneManager obj = FindObjectOfType<GameSceneManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    GameSceneManager newObj = new GameObject().AddComponent<GameSceneManager>();
                    _instance = newObj;
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        GameSceneManager[] obj = FindObjectsOfType<GameSceneManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
