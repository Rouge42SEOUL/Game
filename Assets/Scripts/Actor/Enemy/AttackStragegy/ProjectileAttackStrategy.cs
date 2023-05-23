using Interface;
using Skill.Projectile;
using UnityEngine;

namespace Actor.Enemy
{
    public class ProjectileAttackStrategy : EnemyAttackStrategy
    {
        private GameObject _context;
        private ProjectileLauncher _launcher;
        
        public ProjectileAttackStrategy(Player.Player target, GameObject context) : base(target)
        {
            _context = context;
            _launcher = _context.GetComponent<Enemy>().Launcher;
            _launcher.SetContext(_context);
        }

        public override void Attack(DamageData data)
        {
            _launcher.Launch();
        }
    }
}