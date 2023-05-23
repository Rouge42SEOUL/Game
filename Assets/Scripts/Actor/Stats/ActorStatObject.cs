using System;
using System.Collections.Generic;
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
            baseAttributes[AttributeType.MoveSpeed] = baseAttributes[AttributeType.Speed];
            baseAttributes[AttributeType.AttackSpeed] = baseAttributes[AttributeType.Speed];
            baseAttributes[AttributeType.Accuracy] = baseAttributes[AttributeType.Health];
            baseAttributes[AttributeType.Avoidance] = baseAttributes[AttributeType.Speed];
            
            baseHealthPoint = baseAttributes[AttributeType.Health].value * 100;
        }
    }
}
        