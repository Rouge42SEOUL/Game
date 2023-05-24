using System.Collections.Generic;
using GameScene;
using Managers.DataManager;
using Newtonsoft.Json;
using UnityEngine;

public partial class MapDataManager // public
{
    [SerializeField] private int firstGold;
    [SerializeField] private PlayerPawn playerPawn;
    [SerializeField] private string jsonFileName = "Json/GameManager.json";

    public void SetEventRunning()
    {
        _infoToJson.IsEventRunning = true;
    }

    public int Gold
	{
	    get => _infoToJson.Gold;
        set
	    {
            _infoToJson.Gold = value;
			_uiManager.goldUI.Gold = _infoToJson.Gold;
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
        _infoToJson.Map = _stageManager.MapNum;
        _infoToJson.SaveInfo(_stageManager.Nodes, _currentNode);
		JsonConverter.Save(_infoToJson, Application.dataPath + jsonFileName);
	}
}

public partial class MapDataManager : MonoBehaviour // private
{
    private EventManager _eventManager;
    private StageManager _stageManager;
	private UIManager _uiManager;
    private DataContainer _infoToJson;
    private Node _currentNode;
    private bool _isFirstStart;

    private void Start()
    {
		// Init
        _eventManager = EventManager.Instance;
        _stageManager = StageManager.Instance;
		_uiManager = UIManager.Instance;
		// Load Prev Data
        _isFirstStart = !JsonConverter.Load(out _infoToJson, Application.dataPath + jsonFileName);
        if (_isFirstStart)
        {
            _stageManager.RandomInit();
            Gold = firstGold;
        }
        else
        {
            _stageManager.PrevInit(_infoToJson);
			Gold = _infoToJson.Gold;
		}
		_uiManager.goldUI.Gold = Gold;
        _currentNode = _stageManager.Nodes[_infoToJson.PlayerCurrentNode];
        playerPawn.MoveToNode(_currentNode);
        SaveCurrentInfo();
    }
}
public partial class MapDataManager // singleton
{
    private static MapDataManager _instance;

    public static MapDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                MapDataManager obj = FindObjectOfType<MapDataManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    MapDataManager newObj = new GameObject().AddComponent<MapDataManager>();
                    _instance = newObj;
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        MapDataManager[] obj = FindObjectsOfType<MapDataManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
