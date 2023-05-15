using System.Collections;
using UnityEngine;

namespace Interface
{
    public interface IActorContext
    {
        GameObject AttackCollider { get; }
        Vector3 Position { get; }
        Vector2 Forward { get; }
    }
}