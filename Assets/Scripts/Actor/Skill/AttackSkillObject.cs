using Actor.Player;
using Interface;
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
                    context.AttackCollider.SetActive(true);
                    break;
                }
                case TargetType.World:
                {
                    // get all enemies from spawner
                    break;
                }

            }
        }

        public override void Cancel()
        {
            
        }
        
        private void _SetAttackCol()
        {
            Vector2 front = context.Forward;
            Vector2 t = new Vector2(Mathf.Abs(front.y), Mathf.Abs(front.x));
            attackTransform.localScale = t * 0.5f + new Vector2(1, 1);
            attackTransform.localPosition = front * 0.5f;
        }

        public void OnAttackTrigger(GameObject target)
        {
            // TOD0 : optimize and reimplement KnockBackForce power
            _damage.kbForce = Vector3.Normalize(target.transform.position - context.Position);
            target.GetComponent<IDamageable>().GetHit(_damage);
        }
    }
}