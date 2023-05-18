using Actor.Stats;
using Actor.Skill.Strategy;
using Elemental;
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
        
        public void EffectDetermination(Actor<ActorStatObject> target)
        {
            switch (_damage.ElementalType)
            {
                case ElementalType.Fire:
                {
                    if (target.stat.elementalType == ElementalType.Wind)
                        _damage.Damage *= 2;
                    var effect = new Effect(EffectType.Burns, 10, 5);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Ice:
                {
                    if (target.stat.elementalType == ElementalType.Fire)
                        _damage.Damage *= 2;
                    var effect = new Effect(EffectType.Frostbite, 10, 5);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Ground:
                {
                    if (target.stat.elementalType == ElementalType.Ice)
                        _damage.Damage *= 2;
                    var effect = new Effect(EffectType.Fracture, 5);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Wind:
                {
                    if (target.stat.elementalType == ElementalType.Ground)
                        _damage.Damage *= 2;
                    var effect = new Effect(EffectType.Bleeding, 5);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Holy:
                {
                    if (target.stat.elementalType == ElementalType.Dark)
                        _damage.Damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Dark:
                {
                    if (target.stat.elementalType == ElementalType.Holy)
                        _damage.Damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5);
                    AffectedConfirm(target, effect);
                    break;
                }
            }
        }
        
         private static void AffectedConfirm(Actor<ActorStatObject> target, Effect effect)
         {
             var rand = new Unity.Mathematics.Random();
             var randDouble = rand.NextDouble();
             if (effect.type is EffectType.Bleeding or EffectType.Fracture)
             {
                 if(randDouble < 0.01f)
                     Affected(target, effect);
             }
             else
             {
                 if(randDouble < 0.1f)
                     Affected(target, effect);
             }
         }
         
         private static void Affected(Actor<ActorStatObject> target, Effect effect)
         {
             target.skillEffectValues[effect.effectTo[0]] = effect.effectValue;
             target.stat.effects.Add(effect);
         }
    }
    
}