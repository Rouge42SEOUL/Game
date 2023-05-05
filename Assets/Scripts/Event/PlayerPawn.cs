using UnityEngine;
using UnityEngine.UI;

public class PlayerPawn : MonoBehaviour
{
    public void MovePlayerPawn(Node eNode)
    {
        transform.position = eNode.transform.position;
    }
}
