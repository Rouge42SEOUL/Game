using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private LinkedListNode<int>[] _nextNode;
    public LinkedListNode<int> _eventNode;
    public int _positionX;
    public int _positionY; // Round 계층
    public EventType _eventType;
    private Color[] _colors = new Color[6]
        {Color.yellow, Color.cyan, Color.blue, Color.green, Color.gray, Color.black};

    public void ChangeColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _colors[(int)_eventType];
    }
}
