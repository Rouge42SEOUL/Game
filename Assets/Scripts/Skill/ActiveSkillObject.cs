using Actor.Stats;
using Interface;
using Skill.Strategy;
using UnityEngine;

namespace Skill
{
    public abstract class ActiveSkillObject : SkillObject
    {
        [SerializeField] protected TargetType targetType;
        [SerializeField] protected bool hasDotDamage = false;
        [SerializeField] protected bool hasEffect = false;
        [SerializeField] protected bool isMultiplication = false;
        
        [SerializeField] protected DamageData dotDamage;
        [SerializeField] protected float dotDuration;
        
        public Effect effect;
        protected SkillStrategy strategy;
        
        [SerializeField] protected float coolTime;
        [SerializeField] protected float activeSpeed;
        [SerializeField] protected float range;
        
        [SerializeField] protected float changeValueE;
        [SerializeField] protected float changeValueC;
        [SerializeField] protected float changeValueA;
        [SerializeField] protected float changeValueD;

        protected abstract void InitSkill();

        public override void SetContext(IActorContext actor)
        {
            base.SetContext(actor);
            InitSkill();
        }

        public abstract override void Use();

        public virtual void LevelUp()
        {
            this.level++;
            this.effect.effectValue += changeValueE;
            this.coolTime -= changeValueC;
            this.activeSpeed -= changeValueA;
            this.dotDuration += changeValueD;
        }
        
        public override void Cancel()
        {
            context.AttackCollider.SetActive(false);
        }

        protected GameObject GetTarget()
        {
            return null;
        }

        protected float Add(float targetValue) => effect.effectValue;
        protected float Multiply(float targetValue) => targetValue * effect.effectValue;
    }
}