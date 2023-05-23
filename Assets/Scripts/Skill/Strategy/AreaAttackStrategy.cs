using Interface;
using UnityEngine;

namespace Skill.Strategy
{
    public class AreaAttackStrategy : SkillStrategy
    {
        public AreaAttackStrategy(IActorContext context) : base(context)
        {}
        
        public override void Use()
        {
            SetAttackCol();
            context.AttackCollider.SetActive(true);
        }
        
        private void SetAttackCol()
        {
            var front = context.Forward;
            var t = new Vector2(Mathf.Abs(front.y), Mathf.Abs(front.x));
            var attackTransform = context.AttackCollider.transform;
            attackTransform.localScale = t * 0.5f + new Vector2(1, 1);
            attackTransform.localPosition = front * 0.5f;
        }
    }
}