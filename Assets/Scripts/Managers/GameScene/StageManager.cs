using Managers.DataManager;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RoundTable
{
    Start = 1,
    Middle = 5,
    End = 15,
}

public partial class StageManager // public
{
    public GameObject[] map;
    public EventList[] eventLists;
    public int MapNum { get; private set; }
    public Node[] Nodes { get; private set; }
     
    // 첫시작시 랜덤으로 맵, 노드 초기화
    public void RandomInit()
    {
        RandomStage();
        RandomSetting();
    }
    // 이전데이터를 읽어서 그 데이터로 stage를 구성
    public void PrevInit()
    {
        // 맵 설정
        MapNum = DataManager.Instance.MapIndex;
        _selectMap = map[MapNum];
        _selectMap = Instantiate(_selectMap);
        // 노드들 설정
        int nodeCount = DataManager.Instance.Events.Count;
        int[] keys = new int[nodeCount];
        DataManager.Instance.InitEventKeys(ref keys);
        Nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            int key = keys[i];
            Nodes[key] = _selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
            Nodes[key].eventType = DataManager.Instance.Events[key];
            Nodes[key].EventSetting();
        }
    }
}

public partial class StageManager : MonoBehaviour // private
{
    private GameObject _selectMap;

    private void RandomStage()
    {
        MapNum = Random.Range(0, map.Length);
        _selectMap = map[MapNum];
        _selectMap = Instantiate(_selectMap);
    }

    private void RandomSetting()
    {
        int nodeCount = _selectMap.transform.childCount - 4; // -1은  화살표 오브젝트들 때문
        Nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            Nodes[i] = _selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
            if (Nodes[i].positionY == 0)
            {
                Nodes[i].eventType = EventType.None;
            }
            else if (Nodes[i].positionY == (int)RoundTable.Start) // 시작은 배틀로 고정
            {
                Nodes[i].eventType = EventType.Battle;
            }
            else if (Nodes[i].positionY == (int)RoundTable.End) // 마지막은 보스로 고정
            {
                Nodes[i].eventType = EventType.Boss;
            }
            else if (Nodes[i].positionY <= (int)RoundTable.Middle) // 처음부터 5라운드까지
            {
                EventList eventList = eventLists[0];
                float num = Random.Range(0.0f, 1.0f);
                foreach (EventStruct e in eventList.eventStructs) // 이벤트 갯수만큼 반복문
                {
                    if (num <= e.probability) // 랜덤값이 어떤 이벤트 구간인지 확인
                    {
                        Nodes[i].eventType = e.type;
                        break;
                    }
                }
            }
            else    // 5라운드부터 마지막까지
            {
                EventList eventList = eventLists[1];
                float num = Random.Range(0.0f, 1.0f);
                foreach (EventStruct e in eventList.eventStructs)
                {
                    if (num <= e.probability)
                    {
                        Nodes[i].eventType = e.type;
                        break;
                    }
                }
            }
            Nodes[i].EventSetting();
        }
    }
}

public partial class StageManager // singleton
{
    private static StageManager _instance;
    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                StageManager obj = FindObjectOfType<StageManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    StageManager newObj = new GameObject().AddComponent<StageManager>();
                    _instance = newObj;
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        StageManager[] obj = FindObjectsOfType<StageManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}