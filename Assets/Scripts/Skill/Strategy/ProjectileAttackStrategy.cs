using Interface;
using Skill.Projectile;
using UnityEngine;

namespace Skill.Strategy
{
    public class ProjectileAttackStrategy : SkillStrategy
    {
        private GameObject _projectile;

        public ProjectileAttackStrategy(IActorContext context, ref GameObject projectile) : base(context)
        {
            _projectile = projectile;
            base.context.Launcher.SetProjectile(_projectile);
        }
        
        public override void Use()
        {
            context.Launcher.Launch();
        }
    }
}