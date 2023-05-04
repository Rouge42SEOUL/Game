using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] nextNode;
    private LinkedListNode<Node> _eventNode;
    public int positionX;
    public int positionY; // Round 계층
    public EventType eventType;
    private readonly Color[] _colors = new Color[7]
        {Color.white, Color.yellow, Color.cyan, Color.blue, Color.green, Color.gray, Color.black};


    public void ChangeColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _colors[(int)eventType];
    }

    public void MoveToNextNode()
    {
        Node node = this;
        node = nextNode[0];
    }
}
