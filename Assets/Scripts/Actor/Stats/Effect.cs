using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Actor.Stats
{
    [Serializable]
    public class Effect
    {
        public EffectType type;
        public bool isStackable;
        public bool isPermanent;
        public float duration;
        public AttributeType effectTo;
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
        
        public Effect(EffectType type, AttributeType effectTo, float effectValue)
        {
            this.type = type;
            isPermanent = true;
            isStackable = false;
            this.effectTo = effectTo;
            this.effectValue = effectValue;
        }

        public Effect(EffectType type, float duration, AttributeType effectTo, float effectValue)
        {
            this.type = type;
            isPermanent = false;
            this.duration = duration;
            this.effectTo = effectTo;
            this.effectValue = effectValue;

            isStackable = type is EffectType.Burns or EffectType.Frostbite or EffectType.Poison;
        }
    }
}