using Interface;
using UnityEngine;

namespace Actor.Enemy
{
    public class CollisionAttackStrategy : EnemyAttackStrategy
    {
        public CollisionAttackStrategy(Player.Player target) : base(target)
        {}

        public override void Attack(ref DamageData data)
        {
            _target.Damaged(data);
        }
    }
}