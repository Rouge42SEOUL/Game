using Actor.Stats;
using Skill.Projectile;
using UnityEngine;

namespace Interface
{
    public interface IActorContext
    {
        float GetAttributeValue(AttributeType type);
        GameObject GameObject { get; }
        GameObject AttackCollider { get; }
        Vector3 Position { get; }
        Vector2 Forward { get; }
        ProjectileLauncher Launcher { get; }
    }
}