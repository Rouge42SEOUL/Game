using System;
using System.Collections.Generic;
using Actor.Skill;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Stats
{
    public abstract class ActorStatObject : ScriptableObject
    {
        protected bool isInitialized = false;
        
        public AttackSkillObject normalAttack;
        public SerializableDictionary<AttributeType, Attribute> baseAttributes = new();
        public List<Effect> effects = new();
        public ElementalType elementalType;

        protected virtual void OnEnable()
        {
            if (isInitialized)
                return;
            isInitialized = true;
            
            Debug.Log("init stat");
            baseAttributes.Clear();
            // TODO: set initial stats
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                baseAttributes[type] = new Attribute(type, 10);
            }
            CalculateSideAttributes();
            effects.Clear();
        }

        protected void CalculateSideAttributes()
        {
            // TODO: calculate 
        }
    }
}