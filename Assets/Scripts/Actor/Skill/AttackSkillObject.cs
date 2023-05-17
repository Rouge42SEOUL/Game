using Interface;
using ObjectPool;
using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Attack")]
    public class AttackSkillObject : ActiveSkillObject
    {
        [SerializeField] private DamageData _damage;
        [SerializeField] private ProjectileData _projectile;
        
        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Projectile:
                {
                    context.Launcher.Launch(_projectile);
                    break;
                }
                case TargetType.Area:
                {
                    SetAttackCol();
                    context.AttackCollider.SetActive(true);
                    break;
                }
                case TargetType.World:
                {
                    var enemies = EnemySpawner.Instance.GetAllEnemies();
                    if (hasEffect)
                    {
                        foreach (var enemy in enemies.Values)
                        {
                            enemy.GetComponent<IDamageable>().Damaged(_damage);
                        }
                    }
                    else
                    {
                        foreach (var enemy in enemies.Values)
                        {
                            enemy.GetComponent<IDamageable>().Damaged(_damage);
                        }
                    }
                    break;
                }

            }
        }

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            _damage.KbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>()?.Damaged(_damage);
            if (hasEffect)
            {
                target.GetComponent<IAffected>()?.Affected(effect, isMultiplication ? Multiply : Add);
            }
        }
    }
}