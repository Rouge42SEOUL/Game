using Interface;
using UnityEngine;

namespace Actor.Skill.Strategy
{
    public class ProjectileAttackStrategy : SkillStrategy
    {
        private ProjectileData _projectile;

        public ProjectileAttackStrategy(IActorContext context, ProjectileData projectile) : base(context)
        {
            _projectile = projectile;
        }
        
        public override void Use()
        {
            context.Launcher.Launch(_projectile);
        }
    }
}