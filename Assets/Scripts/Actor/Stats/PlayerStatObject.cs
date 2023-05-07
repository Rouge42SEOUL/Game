using System;
using System.Collections.Generic;
using Actor.Skill;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Stat/PlayerStat")]
    public class PlayerStatObject : ScriptableObject
    {
        #region Variables

        public List<Attribute> attributes;
        public List<Effect> effects;
        public List<SkillSlot> skills;

        private int _level = 1;
        private int _exp = 0;

        private bool _isInitialized = false;

        private int _baseHealthPoint;
        private int _currentHealthPoint;

        #endregion

        #region Properties

        public int Level => _level;
        public float PercentHealPoint => (_baseHealthPoint > 0) ? (_currentHealthPoint / _baseHealthPoint) : 0;

        #endregion

        #region PrivateMethods

        private void OnEnable()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            
            Debug.Log("init stat");
            attributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                attributes.Add(new Attribute(type, 10));
            }
            effects.Clear();
            // base health point initialize
            skills.Clear();
            for (int i = 0; i < 5; i++)
            {
                skills.Add(new SkillSlot());
            }
        }

        #endregion
        
        #region PublicMethods

        public int GetAttributeValue(AttributeType type)
        {
            foreach (var att in attributes)
            {
                if (att.type == type)
                    return att.currentValue;
            }

            return -1;
        }
        
        public void AddAttributeValue(AttributeType type, int value)
        {
            foreach (var att in attributes)
            {
                if (att.type != type)
                    continue;
                att.currentValue += value;
                return;
            }
        }

        public void AddEffect(EffectType type)
        {
            effects.Add(new Effect(type));
        }

        public void AddEffect(EffectType type, float duration)
        {
            effects.Add(new Effect(type, duration));
        }

        public void AddExp(int value)
        {
            this._exp += value;
            // if exp > max, level++ 
            // add attribute base value by level
        }

        #endregion
    }
}