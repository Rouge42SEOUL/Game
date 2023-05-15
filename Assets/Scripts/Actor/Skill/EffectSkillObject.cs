using System.Collections.Generic;
using UnityEngine;
using Actor.Stats;
using Interface;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Effect")]
    public class EffectSkillObject : ActiveSkillObject
    {
        public IAffected affectTarget;
        [SerializeField] private Effect _effect;

        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Self:
                    affectTarget.Affected(_effect, isMultiplication ? Multiply : Add);
                    break;
                case TargetType.Single:
                {
                    affectTarget = GetTarget().GetComponent<IAffected>();
                    if (isDotEffect)
                        affectTarget.Affected(_effect, isMultiplication ? Multiply : Add);
                    attackTarget.DotDamaged(dotDamage, duration);
                    break;
                }
                case TargetType.Area:
                {
                    List<GameObject> targets;
                    GetTarget(out targets);
                    foreach (var target in targets)
                        target.GetComponent<Actor<ActorStatObject>>().Affected(_effect, isMultiplication ? Multiply : Add);
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
        
        public override void Cancel()
        {
            
        }

    }
}