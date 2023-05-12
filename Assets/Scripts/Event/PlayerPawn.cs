using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    private EventManager _eventManager;

    public void MoveToNode(Node node)
    {
        _eventManager = EventManager.Instance;
        transform.position = node.transform.position;
        _eventManager.EventUISetting(node);
    }
}
