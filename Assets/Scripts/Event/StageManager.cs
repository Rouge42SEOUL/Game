using UnityEngine;
using Random = UnityEngine.Random;

public enum RoundTable
{
    Start = 1,
    Middle = 5,
    End = 13,
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

public partial class StageManager : MonoBehaviour
{

    public GameObject[] map;
    public int MapNum { get; private set; }
    public EventList[] eventLists;
    public GameObject selectMap;
    public Node[] Nodes { get; private set; }

    // 첫시작시 랜덤으로 맵, 노드 초기화
    public void RandomInit()
    {
        RandomStage();
        RandomSetting();
    }
    
    
    // 이전데이터를 읽어서 그 데이터로 stage를 구성
    public void PrevInit(InfoToJson prevData)
    {
        // 맵 설정
        MapNum = prevData.Map;
        selectMap = map[MapNum];
        selectMap = Instantiate(selectMap);
        // 노드들 설정
        int nodeCount = prevData.Events.Count;
        int[] keys = new int[nodeCount];
        prevData.Events.Keys.CopyTo(keys, 0);

        Nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            int key = keys[i];
            Nodes[key] = selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
            Nodes[key].eventType = prevData.Events[key];
            Nodes[key].EventSetting();
        }
    }
    
    private void RandomStage()
    {
        MapNum = Random.Range(0, 2);
        selectMap = map[MapNum];
        selectMap = Instantiate(selectMap);
    }

    private void RandomSetting()
    {
        int nodeCount = selectMap.transform.childCount;
        Nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            Nodes[i] = selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
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
                foreach (EventStruct e in eventList.EventStructs) // 이벤트 갯수만큼 반복문
                {
                    if (num <= e.Probability) // 랜덤값이 어떤 이벤트 구간인지 확인
                    {
                        Nodes[i].eventType = e.Type;
                        break;
                    }
                }
            }
            else    // 5라운드부터 마지막까지
            {
                EventList eventList = eventLists[1];
                float num = Random.Range(0.0f, 1.0f);
                foreach (EventStruct e in eventList.EventStructs)
                {
                    if (num <= e.Probability)
                    {
                        Nodes[i].eventType = e.Type;
                        break;
                    }
                }
            }
            Nodes[i].EventSetting();
        }
    }
}

