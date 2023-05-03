using System;
using System.Collections.Generic;
using Actor.Stats;
using Core;
using UnityEngine;

namespace Actor.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Stat/EnemyStat")]
    public class EnemyStatObject : ScriptableObject
    {
        public SerializableDictionary<AttributeType, int> baseAttributes;
        public List<Effect> effects;
        
        private int _baseHealthPoint;

        private void OnValidate()
        {
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                baseAttributes[type] = 10;
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
    }
}