using Actor.Stats;
using Interface;
using ObjectPool;
using Unity.VisualScripting;
using UnityEngine;

namespace Actor.Skill.Strategy
{
    public class WorldAttackStrategy : SkillStrategy
    {
        private bool _hasEffect;
        private Effect _effect;
        private DamageData _damage;
        
        public WorldAttackStrategy(IActorContext context, DamageData damage) : base(context)
        {
            _damage = damage;
            _hasEffect = false;
        }

        public WorldAttackStrategy(IActorContext context, DamageData damage, Effect effect) : base(context)
        {
            _damage = damage;
            _effect = effect;
            _hasEffect = true;
        }

        public override void Use()
        {
            var enemies = EnemySpawner.Instance.GetAllEnemies();
            if (_hasEffect)
            {
                foreach (var enemy in enemies.Values)
                {
                    enemy.GetComponent<IDamageable>().Damaged(_damage);
                    // enemy.GetComponent<IAffected>().Affected(_effect, isMultiplication ? Multiply<> : Add);
                }
            }
            else
            {
                foreach (var enemy in enemies.Values)
                {
                    enemy.GetComponent<IDamageable>().Damaged(_damage);
                }
            }
        }
        
        private void SetAttackCol()
        {
            var front = context.Forward;
            var t = new Vector2(Mathf.Abs(front.y), Mathf.Abs(front.x));
            var attackTransform = context.AttackCollider.transform;
            attackTransform.localScale = t * 0.5f + new Vector2(1, 1);
            attackTransform.localPosition = front * 0.5f;
        }
    }
}