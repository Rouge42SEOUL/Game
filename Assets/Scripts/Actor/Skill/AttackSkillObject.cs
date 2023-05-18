using Actor.Skill.Strategy;
using Interface;
using ObjectPool;
using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Attack Skill", menuName = "Scriptable Object/Skill/Attack")]
    public class AttackSkillObject : ActiveSkillObject
    {
        [SerializeField] private DamageData _damage;
        [SerializeField] private ProjectileData _projectile;

        protected override void InitSkill()
        {
            switch (targetType)
            {
                case TargetType.Projectile:
                    strategy = new ProjectileAttackStrategy(context, _projectile);
                    break;
                case TargetType.Area:
                    strategy = new AreaAttackStrategy(context);
                    break;
                case TargetType.World:
                    strategy = new WorldAttackStrategy(context, _damage);
                    break;
                case TargetType.Self:
                case TargetType.Single:
                default:
                    Debug.LogError("Active Skill: Target Type Invalid");
                    break;
            }
        }

        public override void Use() => strategy.Use();

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            _damage.KbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>()?.Damaged(_damage);
            if (hasEffect)
            {
                target.GetComponent<IAffected>()?.Affected(effect, isMultiplication ? Multiply : Add);
            }
        }
    }
}