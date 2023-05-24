using System.Collections.Generic;
using GameScene;
using Managers.DataManager;
using Newtonsoft.Json;
using UnityEngine;

public partial class MapDataManager // public
{
    [SerializeField] private PlayerPawn playerPawn;
    public Node CurrentNode => _currentNode;

	public void MovePlayer(Node node)
    {
		_uiManager.OptionUIControl();
        _currentNode = node;
        playerPawn.MoveToNode(node);
        DataManager.Instance.SaveData();
    }
}

public partial class MapDataManager : MonoBehaviour // private
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
        _isFirstStart = !DataManager.Instance.LoadData();
        if (_isFirstStart)
        {
            _stageManager.RandomInit();
            DataManager.Instance.InitData();
        }
        else
        {
            Debug.Log("PrevInit");
            _stageManager.PrevInit();
		}
        _currentNode = _stageManager.Nodes[DataManager.Instance.CurrentNode];
        playerPawn.MoveToNode(_currentNode);
        DataManager.Instance.SaveData();
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
