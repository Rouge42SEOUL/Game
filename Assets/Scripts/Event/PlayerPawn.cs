using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }
    

    public void MoveToNode(Node node)
    {
        transform.position = node.transform.position;
        _eventManager.EventUISetting(node);
    }
}