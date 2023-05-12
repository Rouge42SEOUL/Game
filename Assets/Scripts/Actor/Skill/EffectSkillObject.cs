using System;
using System.Collections.Generic;
using UnityEngine;
using Actor.Stats;
using Interface;


namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Effect")]
    public class EffectSkillObject : ActiveSkillObject
    {
        private IAffected _affectTarget;
        [SerializeField] private EffectType effectType;
        [SerializeField] private bool isStackable;
        [SerializeField] private bool isPermanent;
        [SerializeField] private Effect effect;
        [SerializeField] private bool isRelease;
        

        public override void Use()
        {
            effect.type = this.effectType;
            effect.isPermanent = this.isPermanent;
            effect.isStackable = this.isStackable;
            effect.effectTo = this.effectTo;
            effect.effectValue = this.effectValue;
            effect.duration = this.duration;
            effect.isPercent = this.isPercent;
            switch (targetType)
            {
                case TargetType.Self:
                {
                    if (isRelease)
                        _affectTarget.Affected(effect,isRelease ? Release : Add);
                    else
                        _affectTarget.Affected(effect, isMultiplication ? Multiply : Add);
                    break;
                }
                case TargetType.Single:
                {
                    _affectTarget = GetTarget().GetComponent<IAffected>();
                    if (isDotEffect)
                        _affectTarget.Affected(effect, isMultiplication ? Multiply : Add);
                    AttackTarget.DotDamaged(dotDamage, duration);
                    break;
                }
                case TargetType.Area:
                {
                    List<GameObject> targets;
                    GetTarget(out targets);
                    foreach (var target in targets)
                        target.GetComponent<Actor<ActorStatObject>>().Affected(effect, isMultiplication ? Multiply : Add);
                    break;
                }
                case TargetType.World:
                {
                    // get all enemies from spawner
                    break;
                }
                case TargetType.Projectile:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public override void Cancel()
        {
            
        }

    }
}