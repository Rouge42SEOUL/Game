using System.Collections.Generic;
using UnityEngine;
using Actor.Stats;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Effect")]
    public class EffectSkillObject : ActiveSkillObject
    {
        private float _duration;
        [SerializeField] private Effect _effect;
        
        public EffectSkillObject(GameObject context) : base(context)
        {}

        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Self:
                    context.GetComponent<Actor<ActorStatObject>>().GetEffect(_effect, isMultiplication ? Multiply : Add);
                    break;
                case TargetType.Single:
                {
                    var target = GetTarget().GetComponent<Actor<ActorStatObject>>();
                    if (isDotEffect)
                        target.GetEffect(_effect, isMultiplication ? Multiply : Add);
                    target.GetDotDamage(_duration);
                    break;
                }
                case TargetType.Area:
                {
                    List<GameObject> targets;
                    GetTarget(out targets);
                    foreach (var target in targets)
                        target.GetComponent<Actor<ActorStatObject>>().GetEffect(_effect, isMultiplication ? Multiply : Add);
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