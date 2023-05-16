using System.Collections;
using Actor.Skill;
using UnityEngine;

namespace Interface
{
    public interface IActorContext
    {
        GameObject GameObject { get; }
        GameObject AttackCollider { get; }
        Vector3 Position { get; }
        Vector2 Forward { get; }
        ProjectileLauncher Launcher { get; }
    }
}