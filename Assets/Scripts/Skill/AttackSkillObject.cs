using Actor;
using Actor.Stats;
using Core;
using Interface;
using Skill.Projectile;
using Skill.Strategy;
using UnityEngine;
using Random = System.Random;

namespace Skill
{
    
    [CreateAssetMenu(fileName = "New Attack Skill", menuName = "Scriptable Object/Skill/Attack")]
    public class AttackSkillObject : ActiveSkillObject
    {
        [SerializeField] private DamageData _damage;
        [SerializeField] private ProjectileData _projectile;
        
        private Random _random = new Random();
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
        
        private void CalculateHit(float userAccuracy, float targetAvoid, float targetDefense, DamageData damageData)
        {
            var randomValue = (float)_random.NextDouble();
            var hitChance = userAccuracy - targetAvoid;
            if (randomValue > hitChance)
                damageData.Damage = 0;
            else
            {
                damageData.Damage -= targetDefense;
            }
        }

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            var userAccuracy = context.GetAttributeValue(AttributeType.Accuracy);
            var targetAvoid = target.GetComponent<IActorContext>().GetAttributeValue(AttributeType.Avoidance);
            var targetDefense = target.GetComponent<IActorContext>().GetAttributeValue(AttributeType.Defense);
            CalculateHit(userAccuracy, targetAvoid, targetDefense, _damage);
            _damage.KbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>()?.Damaged(_damage);
            if (hasEffect)
            {
                target.GetComponent<IAffected>()?.Affected(effect);
            }
        }
    }
    
}