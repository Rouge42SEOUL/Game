using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Stats
{
    [Serializable]
    public class Effect
    {
        public EffectType type;
        public bool isStackable;
        public bool isPermanent;
        public float duration;
        public float effectivePoint;
        public SerializableDictionary<AttributeType, float> effectiveValues;
        public int overlappingCount;
        public int DisplayTime
        {
            get
            {
                if (duration >= 60)
                {
                    return Mathf.FloorToInt(duration / 60);
                }
                return Mathf.FloorToInt(duration);
            }
        }
        
        public Effect(EffectType type)
        {
            this.type = type;
            isPermanent = true;
            isStackable = false;
        }

        public Effect(EffectType type, float duration)
        {
            this.type = type;
            isPermanent = false;
            this.duration = duration;

            isStackable = type is EffectType.Burns or EffectType.Frostbite or EffectType.Poison;
        }
    }
}