using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public RougeEvent.Event[] events;
    [SerializeField] private GameObject[] eventSelectionWindow; 
    private Dictionary<EventType, RougeEvent.Event> _events 
        = new Dictionary<EventType, RougeEvent.Event>();
    private void Start()
    {
        foreach (RougeEvent.Event tmpEvent in events)
        {
            _events.Add(tmpEvent.Type, tmpEvent);
        }
    }

    public void EventUISetting(Node node) // next Node를 가져와서 이벤트UI에 띄운다
    {
        int nodeCount = node.nextNode.Length;
        for (int i = nodeCount; i < eventSelectionWindow.Length; i++) // 다음 노드들의 갯수 이외의 버튼들은 꺼준다.
            eventSelectionWindow[i].SetActive(false);
        for (int i = 0; i < nodeCount; i++)
        {
            TextMeshProUGUI tmp = eventSelectionWindow[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tmp.text = node.nextNode[i].eventType.ToString();
            int i1 = i; // 람다식에서  "()"closure를 사용하면 값을 복사 하는게 아닌 참조를 하기때문에 i가 바뀌면 함수의 위치도 바뀌게 된다.
                        // 그래서 각각의 이벤트에 대한 위치 i를 지역변수로 복사한다음 사용해야 한다.
            eventSelectionWindow[i].GetComponent<Button>().onClick.AddListener(() => EventAction(node.nextNode[i1].eventType));
        }
    }

    public void EventAction(EventType eventType)
    {
        if (_events.TryGetValue(eventType, out RougeEvent.Event value) == false)
        {
            Debug.Log("찾고자 하는 이벤트가 없습니다");
        }
        else
        {
            value.BuildUI();
        }
    }

}