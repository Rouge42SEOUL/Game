using System.Collections.Generic;
using Actor.Player;
using Actor.Stats;
using Interface;
using UnityEngine;

namespace Actor.Skill
{
    public abstract class ActiveSkillObject : SkillObject
    {   
        [SerializeField] protected TargetType targetType;
        [SerializeField] protected bool isDotEffect = false;
        [SerializeField] protected bool isMultiplication = false;
        
        [SerializeField] protected float range;
        [SerializeField] protected float duration;
        [SerializeField] protected DamageData dotDamage;
        
        public List<AttributeType> effectTo;
        [SerializeField] protected float effectValue;
        [SerializeField] protected float coolTime;
        [SerializeField] protected float activeSpeed;
        [SerializeField] protected float changeValueE;
        [SerializeField] protected float changeValueC;
        [SerializeField] protected float changeValueA;
        [SerializeField] protected float changeValueD;
            
        public abstract override void Use();

        public virtual void LevelUp()
        {
            this.level++;
            this.effectValue += changeValueE;
            this.coolTime -= changeValueC;
            this.activeSpeed -= changeValueA;
            this.duration += changeValueD;
        }
        
        public override void Cancel()
        {
            context.AttackCollider.SetActive(false);
        }

        protected GameObject GetTarget()
        {
            return null;
        }
        
        protected void SetAttackCol()
        {
            var front = context.Forward;
            var t = new Vector2(Mathf.Abs(front.y), Mathf.Abs(front.x));
            var attackTransform = context.AttackCollider.transform;
            attackTransform.localScale = t * 0.5f + new Vector2(1, 1);
            attackTransform.localPosition = front * 0.5f;
        }

        protected float Add(float targetValue) => effectValue;
        protected float Multiply(float targetValue) => targetValue * effectValue;
        protected float Release(float testcode) => effectValue;//임시 처리(상태이상 해제 설정)

    }
}