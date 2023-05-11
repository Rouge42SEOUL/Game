using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Stats;
using Core;
using Interface;
using UnityEngine;
using Attribute = Actor.Stats.Attribute;

namespace Actor
{
    // Values or methods that other can use
    public abstract partial class Actor<T> where T : ActorStatObject
    {
        public SerializableDictionary<AttributeType, Attribute> currentAttributes;
        [SerializeField] protected T stat;
        
        protected int baseHealthPoint;
        protected int currentHealthPoint;

        protected bool isInitialized = false;
        
        public void GetDotDamage(float duration)
        {
            StartCoroutine(AddDotDamage(duration));
        }
        
        public abstract void GetEffect(Effect effect, Func<float, float> getValueToAdd);
        public abstract void GetHit(DamageData data);
        protected abstract void Died();

        public float GetAttributeValue(AttributeType type) => currentAttributes[type].value;
        
        public void AddAttributeValue(AttributeType type, float value)
        {
            currentAttributes[type].value += value;
        }
        
        public void AddEffect(Effect effect)
        {
            stat.effects.Add(effect);
        }
    }
    
    // Values or methods that other cannot use
    public abstract partial class Actor<T>
    {
        private WaitForSeconds _waitForOneSeconds = new WaitForSeconds(1f);
    }
    
    // body of MonoBehaviour
    public abstract partial class Actor<T> : MonoBehaviour, IDamageable
    {
        protected virtual void OnEnable()
        {
            if (isInitialized)
                return;
            isInitialized = true;
            
            Debug.Log(this.gameObject + " init current stat");
            currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                currentAttributes[type] = new Attribute(type, stat.baseAttributes[type].value);
            }
        }
    }
    
    // body of others
    public abstract partial class Actor<T>
    {
        private IEnumerator AddDotDamage(float duration)
        {
            while (duration > 0)
            {
                // GetHit();
                duration -= Time.deltaTime;
                yield return _waitForOneSeconds;
            }
        }
    }
}


