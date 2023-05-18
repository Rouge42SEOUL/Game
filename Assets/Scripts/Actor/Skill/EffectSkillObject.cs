using System;
using System.Collections.Generic;
using UnityEngine;
using Actor.Stats;
using Interface;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Effect Skill", menuName = "Scriptable Object/Skill/Effect")]
    public class EffectSkillObject : ActiveSkillObject
    {
        [SerializeField] private Effect _effect;

        protected override void InitSkill()
        {
            hasEffect = true;
        }

        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Self:
                    context.GameObject.GetComponent<IAffected>().Affected(_effect, isMultiplication ? Multiply : Add);
                    break;
                case TargetType.Single:
                {
                    var target = GetTarget();
                    if (hasDotDamage)
                        target.GetComponent<IAffected>().Affected(_effect, isMultiplication ? Multiply : Add);
                    target.GetComponent<IDamageable>().DotDamaged(dotDamage, dotDuration);
                    break;
                }
                case TargetType.Area:
                {
                    // SetAttackCol();
                    context.AttackCollider.SetActive(true);
                    break;
                }
                case TargetType.World:
                {
                    // get all enemies from spawner
                    break;
                }
                default:
                    break;
            }
        }

    }
}