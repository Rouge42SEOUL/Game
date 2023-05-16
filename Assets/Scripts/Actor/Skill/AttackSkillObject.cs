using Actor.Stats;
using Interface;
using UnityEngine;


namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Attack")]
    public class AttackSkillObject : ActiveSkillObject
    {
        [SerializeField] private DamageData damage;
        
        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Projectile:
                {
                    break;
                }
                case TargetType.Area:
                {
                    context.AttackCollider.SetActive(true);
                    break;
                }
                case TargetType.World:
                {
                    // get all enemies from spawner
                    break;
                }

            }
        }

        public override void Cancel()
        {
            
        }
        
        private void _SetAttackCol()
        {
            Vector2 front = context.Forward;
            Vector2 t = new Vector2(Mathf.Abs(front.y), Mathf.Abs(front.x));
            attackTransform.localScale = t * 0.5f + new Vector2(1, 1);
            attackTransform.localPosition = front * 0.5f;
        }

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            damage.kbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>().GetHit(damage);
        }
        
        public void EffectDetermination(Actor<ActorStatObject> target)
        {
            switch (damage.elementalType)
            {
                case ElementalType.Fire:
                {
                    if (target.stat.elementalType == ElementalType.Wind)
                        damage.damage *= 2;
                    var effect = new Effect(EffectType.Burns, 10, 5, false);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Ice:
                {
                    if (target.stat.elementalType == ElementalType.Fire)
                        damage.damage *= 2;
                    var effect = new Effect(EffectType.Frostbite, 10, 5, false);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Ground:
                {
                    if (target.stat.elementalType == ElementalType.Ice)
                        damage.damage *= 2;
                    var effect = new Effect(EffectType.Fracture, 5, false);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Wind:
                {
                    if (target.stat.elementalType == ElementalType.Ground)
                        damage.damage *= 2;
                    var effect = new Effect(EffectType.Bleeding, 5, false);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Holy:
                {
                    if (target.stat.elementalType == ElementalType.Dark)
                        damage.damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5, false);
                    AffectedConfirm(target, effect);
                    break;
                }
                case ElementalType.Dark:
                {
                    if (target.stat.elementalType == ElementalType.Holy)
                        damage.damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5, false);
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