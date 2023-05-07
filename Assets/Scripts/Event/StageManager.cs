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
    private Node[] _nodes;
    private EventType _eventType;
    

    private void Start()
    {
        RandomStage();
        Instantiate(_selectMap);
        RandomSetting();
    }
    
    public void RandomStage()
    {
        _selectMap= map[Random.Range(0, 2)];
    }

    public void RandomSetting()
    {
        int nodeCount = _selectMap.transform.childCount;
        _nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            _nodes[i] = _selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
            if (_nodes[i]._positionY == (int)StandardRound.Start)
                _nodes[i]._eventType = EventType.Battle;
            else if (_nodes[i]._positionY == (int)StandardRound.End)
                _nodes[i]._eventType = EventType.Battle;
            else if (_nodes[i]._positionY <= (int)StandardRound.Middle)
            {
                EventList eventList = eventLists[0];
                float num = Random.Range(0.0f, 1.0f);
                foreach (EventStruct e in eventList.EventStructs)
                {
                    if (num <= e.Probability)
                    {
                        _nodes[i]._eventType = e.Type;
                        break;
                    }
                }
            }
            else
            {
                EventList eventList = eventLists[1];
                float num = Random.Range(0.0f, 1.0f);
                foreach (EventStruct e in eventList.EventStructs)
                {
                    if (num <= e.Probability)
                    {
                        _nodes[i]._eventType = e.Type;
                        break;
                    }
                }
            }
        }
    }
}

// 맵이랑 플레이어 말이랑 따로 구성, 맵 노드의 종속되지 않게