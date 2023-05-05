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

    public void EventUISetting(Node node)
    {
        for (int i = node.nextNode.Length; i < eventSelectionWindow.Length; i++)
            eventSelectionWindow[i].SetActive(false);
        for (int i = 0; i < node.nextNode.Length; i++)
        {
            TextMeshProUGUI tmp = eventSelectionWindow[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            tmp.text = node.nextNode[i].eventType.ToString();
            // eventSelectionWindow[i].GetComponent<Button>().onClick.AddListener(() => Debug.Log("1111"));
            eventSelectionWindow[i].GetComponent<Button>().onClick.AddListener(() => EventAction(node.nextNode[0].eventType));
        }
    }

    public void EventAction(EventType eventType)
    {
        Debug.Log("action");
        if (_events.TryGetValue(eventType, out RougeEvent.Event value) == false)
        {
            Debug.Log("찾고자 하는 이벤트가 없습니다");
        }
        else
        {
            value.UIControl();
        }
    }

}