using Interface;
using UnityEngine;

namespace Actor.Enemy
{
    public abstract class EnemyAttackStrategy
    {
        protected Player.Player _target;

        public EnemyAttackStrategy(Player.Player target)
        {
            _target = target;
        }
        
        public abstract void Attack(DamageData data);
    }
}