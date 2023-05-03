using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Stat/PlayerStat")]
    public class PlayerStatObject : ScriptableObject
    {
        #region Variables

        public SerializableDictionary<AttributeType, Attribute> attributes;
        public List<Effect> effects;

        private int _level = 1;
        private int _exp = 0;

        private bool _isInitialized = false;

        private int _baseHealthPoint;
        private int _currentHealthPoint;
        
        public Action<PlayerStatObject> OnChangedStats;
        public Action<PlayerStatObject> OnChangedAttributes;
        public Action<PlayerStatObject> OnChangedEffects;
        
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
            
            attributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
                attributes[type] = new Attribute(type, 10);
            effects.Clear();
            // base health point initialize
        }

        #endregion
        
        #region PublicMethods
        
        public void AddAttributeValue(AttributeType type, int value)
        {
            attributes[type].currentValue += value;
            OnChangedAttributes?.Invoke(this);
        }

        public void AddEffect(EffectType type)
        {
            effects.Add(new Effect(type));
            OnChangedEffects?.Invoke(this);
        }

        public void AddEffect(EffectType type, float duration)
        {
            effects.Add(new Effect(type, duration));
            OnChangedEffects?.Invoke(this);
        }

        public void AddExp(int value)
        {
            this._exp += value;
            // if exp > max, level++ 
            OnChangedStats?.Invoke(this);
        }

        #endregion
    }
}