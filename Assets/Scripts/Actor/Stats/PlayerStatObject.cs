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

        public List<Attribute> attributes = new();
        public List<Effect> effects = new();
        
        public SkillSlot[] skills = new SkillSlot[4];
        public SkillObject passive;
        public SkillObject attack;
        
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
            skills[3].slotType = SkillType.Ultimate;
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

        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
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