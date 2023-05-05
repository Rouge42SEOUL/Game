using UnityEngine;
using Random = UnityEngine.Random;

public enum StandardRound
{
    Start = 1,
    Middle = 5,
    End = 13,
}

public class StageManager : MonoBehaviour
{
    public GameObject[] map;
    public EventList[] eventLists;
    private GameObject _selectMap;
    public Node[] _nodes { get; protected set; }
    private EventType _eventType;

    private void Start()
    {
        RandomStage();
        RandomSetting();
    }
    
    public void RandomStage()
    {
        _selectMap = map[Random.Range(0, 2)];
        _selectMap = Instantiate(_selectMap);
    }

    public void RandomSetting()
    {
        int nodeCount = _selectMap.transform.childCount;
        _nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            _nodes[i] = _selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
            if (_nodes[i].positionY == 0)
            {
                _nodes[i].eventType = EventType.None;
            }
            else if (_nodes[i].positionY == (int)StandardRound.Start) // 시작은 배틀로 고정
            {
                _nodes[i].eventType = EventType.Battle;
            }
            else if (_nodes[i].positionY == (int)StandardRound.End) // 마지막은 보스로 고정
            {
                _nodes[i].eventType = EventType.Boss;
            }
            else if (_nodes[i].positionY <= (int)StandardRound.Middle) // 처음부터 5라운드까지
            {
                EventList eventList = eventLists[0];
                float num = Random.Range(0.0f, 1.0f);
                foreach (EventStruct e in eventList.EventStructs) // 이벤트 갯수만큼 반복문
                {
                    if (num <= e.Probability) // 랜덤값이 어떤 이벤트 구간인지 확인
                    {
                        _nodes[i].eventType = e.Type;
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
                        _nodes[i].eventType = e.Type;
                        break;
                    }
                }
            }
            _nodes[i].EventSetting();
            _nodes[i].ChangeColor();
        }
    }
}

