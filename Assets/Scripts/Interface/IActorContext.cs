using System.Collections;
using Skill.Projectile;
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