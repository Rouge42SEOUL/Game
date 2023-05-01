using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Stat/PlayerStat")]
    public class PlayerStatObject : ScriptableObject
    {
        public List<Attribute> attributes;
        public List<Effect> effects;

        public int level = 1;
        private int _exp = 0;

        private bool _isInitialized = false;

        public Action<PlayerStatObject> OnChangedStats;
        public Action<PlayerStatObject> OnChangedAttributes;
        public Action<PlayerStatObject> OnChangedEffects;

        private int _baseHealthPoint;
        private int _currentHealthPoint;

        public float PercentHealPoint => (_baseHealthPoint > 0) ? (_currentHealthPoint / _baseHealthPoint) : 0;

        private void OnEnable()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            
            attributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
                attributes.Add(new Attribute(type, 10));
            effects.Clear();
        }

        public void AddAttributeValue(AttributeType type, int value)
        {
            foreach (var att in attributes)
            {
                if (att.type == type)
                {
                    att.currentValue += value;
                    OnChangedAttributes?.Invoke(this);
                }
            }
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
    }
}