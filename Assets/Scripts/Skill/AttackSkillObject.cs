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
        
        public void CalculateHit(SerializableDictionary<AttributeType, Attribute> userStat, SerializableDictionary<AttributeType, Attribute> targetStat, DamageData damageData)
        {
            var random = new Random();
            var randomValue = (float)random.NextDouble();
            var hitChance = userStat[AttributeType.Accuracy].value -
                            targetStat[AttributeType.Avoidance].value;
            if (randomValue > hitChance)
                damageData.Damage = 0;
            else
            {
                damageData.Damage -= targetStat[AttributeType.Defense].value;
            }
        }

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            var userStat = target.transform.root.gameObject.GetComponent<ActorStatObject>().baseAttributes;
            var stat = target.GetComponent<ActorStatObject>().baseAttributes;
            _damage.KbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>()?.Damaged(_damage);
            if (hasEffect)
            {
                target.GetComponent<IAffected>()?.Affected(effect);
            }
        }
    }
    
}