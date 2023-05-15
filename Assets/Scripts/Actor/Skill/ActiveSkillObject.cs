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
        
        public IDamageable attackTarget;
        [SerializeField] protected Transform attackTransform;
        [SerializeField] protected float range;
        [SerializeField] protected float duration;
        [SerializeField] protected DamageData dotDamage;
        
        public AttributeType effectTo;
        public float effectValue;


        public abstract override void Use();

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
        
    }
}