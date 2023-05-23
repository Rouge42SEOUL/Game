using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Stats
{
    [Serializable]
    public class Effect
    {
        public EffectType type;
        
        public bool isPermanent;
        public float duration;
        
        public List<AttributeType> effectTo;
        public bool isRelease;
        public bool isMultiplication = false;
        public float effectValue;
        
        public bool isStackable;
        public int stackCount;

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
        
        public Effect(EffectType type, float effectValue)
        {
            this.type = type;
            isPermanent = true;
            isStackable = false;
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

        public Effect(EffectType type, float duration, float effectValue)
        {
            this.type = type;
            isPermanent = false;
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
        
        private float Add(float targetValue) => effectValue;
        private float Multiply(float targetValue) => targetValue * effectValue;

        public float GetValueToAdd(float targetValue)
        {
            return isMultiplication ? Multiply(targetValue) : Add(targetValue);
        }
    }
}