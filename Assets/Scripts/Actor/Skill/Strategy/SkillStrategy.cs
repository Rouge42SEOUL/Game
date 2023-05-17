using Interface;

namespace Actor.Skill.Strategy
{
    public abstract class SkillStrategy
    {
        protected IActorContext context;

        public SkillStrategy(IActorContext context)
        {
            this.context = context;
        }
        
        public abstract void Use();
    }
}