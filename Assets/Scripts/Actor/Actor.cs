using System;
using System.Collections;
using Actor.Player;
using Actor.Stats;
using Core;
using Interface;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = Actor.Stats.Attribute;

namespace Actor
{
    // Values or methods that other can use
    public abstract partial class Actor<T> where T : ActorStatObject
    {
        public SerializableDictionary<AttributeType, Attribute> currentAttributes;
        [SerializeField] protected SerializableDictionary<AttributeType, float> skillEffectValues;
        [SerializeField] protected T stat;
        
        protected int baseHealthPoint;
        protected int currentHealthPoint;

        protected bool isInitialized = false;

        public GameObject attackCollider;
        public Vector2 forwardVector;
        
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
        private readonly WaitForSeconds _waitForOneSeconds = new WaitForSeconds(1f);
    }
    
    // body of MonoBehaviour
    public abstract partial class Actor<T> : MonoBehaviour, IActorContext, IDamageable, IAffected
    {
        protected virtual void Awake()
        {
            attackCollider = transform.GetChild(0).gameObject;
            attackCollider.gameObject.SetActive(false);
        }

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
        public GameObject GameObject => gameObject;
        
        public GameObject AttackCollider => attackCollider;
        public Vector2 Forward => forwardVector;
        public Vector3 Position => transform.position;
        
        public void Affected(Effect effect, Func<float, float> getValueToAdd)
        {
            skillEffectValues[effect.effectTo] = getValueToAdd(skillEffectValues[effect.effectTo]);
            stat.effects.Add(effect);
            // TODO: set current attributes
            // TODO: event call 
        }

        public abstract void GetHit(DamageData data);
        public void DotDamaged(DamageData damage, float duration)
        {
            StartCoroutine(AddDotDamage(damage, duration));
        }
        
        private IEnumerator AddDotDamage(DamageData damage, float duration)
        {
            while (duration > 0)
            {
                GetHit(damage);
                duration -= Time.deltaTime;
                yield return _waitForOneSeconds;
            }
        }
    }
}


