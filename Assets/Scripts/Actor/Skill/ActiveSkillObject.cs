using Actor.Skill.Strategy;
using Actor.Stats;
using Interface;
using UnityEngine;

namespace Actor.Skill
{
    public abstract class ActiveSkillObject : SkillObject
    {
        [SerializeField] protected TargetType targetType;
        [SerializeField] protected bool hasDotDamage = false;
        [SerializeField] protected bool hasEffect = false;
        [SerializeField] protected bool isMultiplication = false;
        
        [SerializeField] protected float range;
        [SerializeField] protected DamageData dotDamage;
        [SerializeField] protected float dotDuration;

        public Effect effect;
        protected SkillStrategy strategy;

        protected abstract void InitSkill();

        public override void SetContext(IActorContext actor)
        {
            base.SetContext(actor);
            InitSkill();
        }

        public abstract override void Use();

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