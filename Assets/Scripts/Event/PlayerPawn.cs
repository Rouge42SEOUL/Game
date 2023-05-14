using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    private EventManager _eventManager;

    public void MoveToNode(Node node)
    {
        _eventManager = EventManager.Instance;
		Vector3 newPosition = node.transform.position;
		newPosition.z = transform.position.z; // z 값이 자꾸 10으로 변경되서 z값을 이전 값으로 변경 왜 10으로 바뀌는지는 모름
		transform.position = newPosition;
        _eventManager.EventUISetting(node);
    }
}
