using Interface;
using UnityEngine;

namespace Actor.Enemy
{
    public class CollisionAttackStrategy : EnemyAttackStrategy
    {
        public CollisionAttackStrategy(Player.Player target) : base(target)
        {}

        public override void Attack(DamageData data)
        {
            Debug.Log(_target + "to" + data.Damage);
            _target.Damaged(data);
        }
    }
}