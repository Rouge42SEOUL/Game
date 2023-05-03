using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LinkedListNode<int> _eventNode;
    public int _positionX;
    public int _positionY; // Round 계층
    private LinkedListNode<int>[] _nextNode;
    public EventType _eventType;
}