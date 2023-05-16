using Actor.Player;
using Interface;
using ObjectPool;
using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Attack")]
    public class AttackSkillObject : ActiveSkillObject
    {
        [SerializeField] private DamageData _damage;
        
        public override void Use()
        {
            switch (targetType)
            {
                case TargetType.Projectile:
                {
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
                    foreach (var enemy in enemies.Values)
                    {
                        enemy.GetComponent<IDamageable>().Damaged(_damage);
                    }
                    break;
                }

            }
        }

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            _damage.KbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>().Damaged(_damage);
        }
    }
}