using Actor.Stats;
using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Actor/Skill/Effect")]
    public class EffectSkillObject : ActiveSkillObject
    {
        private float _duration;
        [SerializeField] private AttributeType _effectTo;
        
        public EffectSkillObject(GameObject context) : base(context)
        {}

        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Self:
                    context.GetComponent<Actor>().GetEffect(_effectTo, 1.1f);
                    break;
                case TargetType.Single:
                    break;
                case TargetType.Area:
                    break;
                case TargetType.World:
                    break;
                default:
                    break;
            }
        }
        
        public override void Cancel()
        {
            
        }

    }
}