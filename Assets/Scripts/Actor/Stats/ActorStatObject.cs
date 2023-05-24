using System;
using Core;
using Elemental;
using Skill;
using UnityEngine;

namespace Actor.Stats
{
    public abstract class ActorStatObject : ScriptableObject
    {
        [SerializeField] protected bool isInitialized = false;
        
        public AttackSkillObject normalAttack;
        public SerializableDictionary<AttributeType, Attribute> baseAttributes = new();
        public ElementalType elementalType;
        
        public float baseHealthPoint;

        protected abstract void InitElementalType();

        protected virtual void OnEnable()
        {
            if (isInitialized)
                return;
            isInitialized = true;
            
            baseAttributes.Clear();
            // TODO: set initial stats
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                baseAttributes[type] = new Attribute(type, 10);
            }
            CalculateSideAttributes();
            InitElementalType();
        }

        protected void CalculateSideAttributes()
        {
            baseAttributes[AttributeType.MoveSpeed].value = baseAttributes[AttributeType.Speed].value * 1.25f;
            baseAttributes[AttributeType.AttackSpeed].value = baseAttributes[AttributeType.Speed].value * 1.15f;
            baseAttributes[AttributeType.Accuracy].value = 0.7f + (baseAttributes[AttributeType.Speed].value * 0.01f);
            baseAttributes[AttributeType.Avoidance].value = baseAttributes[AttributeType.Speed].value * 0.0075f;
            
            baseHealthPoint = baseAttributes[AttributeType.Health].value * 100;
        }
    }
}
        