using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Stat/EnemyStat")]
    public class EnemyStatObject : ScriptableObject
    {
        public List<int> baseAttributes = new();
        public List<Effect> effects = new();
        
        private int _baseHealthPoint;
        private bool _isInitialized = false;

        private void OnEnable()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            
            Debug.Log("init enemy");
            baseAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                baseAttributes.Add(10);
            }
            effects.Clear();
            // base health point initialize
        }
        
        public void AddEffect(EffectType type)
        {
            effects.Add(new Effect(type));
        }

        public void AddEffect(EffectType type, float duration)
        {
            effects.Add(new Effect(type, duration));
        }
    }
}