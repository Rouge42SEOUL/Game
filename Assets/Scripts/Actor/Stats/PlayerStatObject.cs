using System;
using System.Collections.Generic;
using Core;
using Elemental;
using Skill;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Stat/PlayerStat")]
    public class PlayerStatObject : ActorStatObject
    {
        #region Variables
        
        public AttackSkillObject normalAttack;
        public SkillSlot[] skills = new SkillSlot[4];
        public SkillObject passive;
        
        public SerializableDictionary<EffectType, Effect> effects = new();
        
        private int _level = 1;
        private int _exp = 0;

        public SerializableDictionary<AttributeType, Attribute> currentAttributes = new();
        public float currentHealthPoint;

        #endregion

        #region Properties

        public int Level => _level;
        public float PercentHealPoint => (baseHealthPoint > 0) ? (currentHealthPoint / baseHealthPoint) : 0;

        #endregion

        #region PrivateMethods
        
        protected override void InitElementalType()
        {
            elementalType = passive ? passive.elementalType : ElementalType.Normal;
        }
        
        #endregion

        #region MonoBehaviourMethods
        protected override void OnEnable()
        {
            if (isInitialized)
                return;
            
            base.OnEnable();
            effects.Clear();
            currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                currentAttributes[type] = new Attribute(type, baseAttributes[type].value);
            }
            
            skills[3].slotType = SkillType.Ultimate;
            currentHealthPoint = baseHealthPoint;
        }

        #endregion
        
        #region PublicMethods

        public void AddExp(int value)
        {
            this._exp += value;
            // if exp > max, level++ 
            // add attribute base value by level
        }

        public void AddAttribute(AttributeType type, float value)
        {
            currentAttributes[type].value += value;
        }

        public void AddEffect(Effect effect)
        {
            
        }

        public void DeleteEffect(EffectType type)
        {
            
        }
        
        #endregion
    }
}