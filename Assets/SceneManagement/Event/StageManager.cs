using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    public GameObject[] _map;
    private GameObject _selectMap;
    private Node[] _nodes;
    private List<EventList> _eventList =  new List<EventList>();
    private EventType _eventType;
    public EventList[] eventLists;
    

    private void Start()
    {
        RandomStage();
        GameObject tmpMap = Instantiate(_selectMap);
        RandomSetting();
    }
    
    public void RandomStage()
    {
        _selectMap= _map[Random.Range(0, 2)];
    }

    private void CalculProbability()
    {
        int eventCount = Enum.GetValues(typeof(EventType)).Length;
        
    }
    public void RandomSetting()
    {
        _nodes = new Node[_selectMap.transform.childCount];
        for (int i = 0; i < _selectMap.transform.childCount; i++)
        {
            _nodes[i] = _selectMap.transform.GetChild(i).gameObject.GetComponent<Node>();
            if (_nodes[i]._positionY == 1)
                _nodes[i]._eventType = EventType.Battle;
            else if (_nodes[i]._positionY == 13) // 15
                _nodes[i]._eventType = EventType.Battle;
            else if (_nodes[i]._positionY <= 5)
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
                Debug.Log("random " + num);
                foreach (EventStruct e in eventList.EventStructs)
                {
                    Debug.Log("Probability " + e.Probability);
                    if (num <= e.Probability)
                    {
                        _nodes[i]._eventType = e.Type;
                        Debug.Log(e.Type);
                        break;
                    }
                }
            }
        }
    }
}
