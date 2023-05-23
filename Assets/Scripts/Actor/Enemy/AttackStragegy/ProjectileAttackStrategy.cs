using Interface;
using Skill.Projectile;
using UnityEngine;

namespace Actor.Enemy
{
    public class ProjectileAttackStrategy : EnemyAttackStrategy
    {
        private GameObject _context;
        private ProjectileLauncher _launcher;
        private DamageData _data;
        
        public ProjectileAttackStrategy(Player.Player target, GameObject context) : base(target)
        {
            _context = context;
            _launcher = _context.GetComponent<Enemy>().Launcher;
            _launcher.SetContext(_context);
            _launcher.OnAttackTrigger += OnAttackTrigger;
        }

        public override void Attack(DamageData data)
        {
            _data = data;
            _launcher.Launch();
        }

        private void OnAttackTrigger(GameObject gameObject)
        {
            _target.GetComponent<IDamageable>()?.Damaged(_data);
        }
    }
}