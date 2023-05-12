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

        public IDamageable AttackTarget;
        [SerializeField] protected Transform attackTransform;
        [SerializeField] protected float range;
        [SerializeField] protected float duration;
        [SerializeField] protected DamageData dotDamage;
        
        public List<AttributeType> effectTo;
        [SerializeField] protected bool isPercent = false;
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

        protected GameObject GetTarget()
        {
            return null;
        }
        
        protected void GetTarget(out List<GameObject> targets)
        {
            targets = new List<GameObject>();
        }

        protected float Add(float targetValue) => effectValue;
        protected float Multiply(float targetValue) => targetValue * effectValue;
        protected float Release(float testcode) => effectValue;//임시 처리(상태이상 해제 설정)

    }
}