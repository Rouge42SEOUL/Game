using System.Collections.Generic;
using Actor.Stats;
using UnityEngine;

namespace Actor.Skill
{
    public abstract class ActiveSkillObject : SkillObject
    {
        [SerializeField] protected TargetType targetType;
        [SerializeField] protected bool isDotEffect = false;
        [SerializeField] protected bool isMultiplication = false;
        public AttributeType effectTo;
        public float effectValue;
        
        public ActiveSkillObject(GameObject context) : base(context)
        {
            targetType = TargetType.Single;
        }

        public abstract override void Use();

        protected GameObject GetTarget()
        {
            return context;
        }
        
        protected void GetTarget(out List<GameObject> targets)
        {
            targets = new List<GameObject>();
        }

        protected float Add(float targetValue) => effectValue;
        protected float Multiply(float targetValue) => targetValue * effectValue;
    }
}