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
        
        public Effect(EffectType type, float effectValue, bool isPercent)
        {
            this.type = type;
            isPermanent = true;
            isStackable = false;
            this.isPercent = isPercent;
            this.effectValue = effectValue;
            this.effectTo = new List<AttributeType>();

            switch (this.type)
            {
                case EffectType.Bleeding :
                    effectTo.Add((AttributeType.Health));
                    break;
                case EffectType.Fracture :
                    effectTo.Add(AttributeType.MoveSpeed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Effect(EffectType type, float duration, float effectValue, bool isPercent)
        {
            this.type = type;
            isPermanent = false;
            this.isPercent = isPercent;
            this.duration = duration;
            this.effectValue = effectValue;
            this.effectTo = new List<AttributeType>();

            isStackable = type is EffectType.Burns or EffectType.Frostbite or EffectType.Poison;
            switch (this.type)
            {
                case EffectType.Burns or EffectType.Poison :
                {
                    effectTo.Add(AttributeType.Health);
                    break;
                }
                case EffectType.Confuse or EffectType.Frostbite :
                {
                    effectTo.Add(AttributeType.MoveSpeed);
                    break;
                }
                case EffectType.Paralysis :
                {
                    effectTo.Add(AttributeType.AttackSpeed);
                    break;
                }
                case EffectType.Blind :
                {
                    effectTo.Add(AttributeType.Accuracy);
                    break;
                }
            }
        }
    }
}