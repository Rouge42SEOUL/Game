using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class EventManager // public
{
    public RougeEvent.Event[] events;
    [SerializeField] private GameObject[] eventSelectionWindow; 

    public void EventUISetting(Node node) // next Node를 가져와서 이벤트UI에 띄운다
    {
        int nodeCount = node.nextNode.Length;
        for (int i = nodeCount; i < eventSelectionWindow.Length; i++) // 다음 노드들의 갯수 이외의 버튼들은 꺼준다.
            eventSelectionWindow[i].SetActive(false);
        for (int i = 0; i < nodeCount; i++)
        {
            eventSelectionWindow[i].SetActive(true);
            TextMeshProUGUI tmp = eventSelectionWindow[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tmp.text = node.nextNode[i].eventType.ToString();
            int i1 = i; // 람다식에서  "()"closure를 사용하면 값을 복사 하는게 아닌 참조를 하기때문에 i가 바뀌면 함수의 위치도 바뀌게 된다.
                        // 그래서 각각의 이벤트에 대한 위치 i를 지역변수로 복사한다음 사용해야 한다.
            eventSelectionWindow[i].GetComponent<Button>().onClick.AddListener(() => EventAction(node.nextNode[i1]));
        }
    }

    public void EventAction(Node node)
    {
        if (_events.TryGetValue(node.eventType, out RougeEvent.Event value) == false)
        {
            Debug.Log("찾고자 하는 이벤트가 없습니다");
        }
        else
        {
            // OnClick에 달린 모든 이벤트들을 해제한다. 이를 안하면 이전에 등록되어있는 이벤트도 실행된다.
            foreach (GameObject obj in eventSelectionWindow)
                obj.GetComponent<Button>().onClick.RemoveAllListeners();
            value.BuildUI();
            _gameManager.MovePlayer(node);
        }
    }
}
public partial class EventManager : MonoBehaviour // private
{
    private readonly Dictionary<EventType, RougeEvent.Event> _events 
        = new Dictionary<EventType, RougeEvent.Event>();
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        foreach (RougeEvent.Event tmpEvent in events)
        {
            _events.Add(tmpEvent.Type, tmpEvent);
        }
    }
}

public partial class EventManager // singleton
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                EventManager obj = FindObjectOfType<EventManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    EventManager newObj = new GameObject().AddComponent<EventManager>();
                    _instance = newObj;
                }
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        EventManager[] obj = FindObjectsOfType<EventManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
