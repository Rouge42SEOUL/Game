using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Stats
{
    [Serializable]
    public class Effect
    {
        public EffectType type;
        public bool isStackable;
        public bool isPermanent;
        public bool isPercent;
        public float duration;
        public List<AttributeType> effectTo;
        public float effectValue;

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
        
        public Effect(EffectType type, float effectValue, bool isPercent)
        {
            this.type = type;
            isPermanent = true;
            isStackable = false;
            this.isPercent = isPercent;
            this.effectValue = effectValue;
        }

        public Effect(EffectType type, float duration, float effectValue, bool isPercent)
        {
            this.type = type;
            isPermanent = false;
            this.isPercent = isPercent;
            this.duration = duration;
            this.effectValue = effectValue;

            isStackable = type is EffectType.Burns or EffectType.Frostbite or EffectType.Poison;
        }
    }
}