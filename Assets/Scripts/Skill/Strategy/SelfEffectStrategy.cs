using Interface;

namespace Skill.Strategy
{
    public class SelfEffectStrategy : SkillStrategy
    {
        public SelfEffectStrategy(IActorContext context) : base(context)
        {}

        public override void Use()
        {
            
        }
    }
}