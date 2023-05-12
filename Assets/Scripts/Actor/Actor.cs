using System;
using System.Collections;
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
        [SerializeField] protected SerializableDictionary<AttributeType, float> skillEffectValues;
        [SerializeField] protected T stat;
        
        protected int BaseHealthPoint;
        protected int CurrentHealthPoint;

        protected bool IsInitialized = false;

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
            if (IsInitialized)
                return;
            IsInitialized = true;
            
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
        
        public GameObject AttackCollider => attackCollider;
        public Vector2 Forward => forwardVector;
        public Vector3 Position => transform.position;
        
        public void Affected(Effect effect, Func<float, float> getValueToAdd)
        {
            for (int i = 0; i < effect.effectTo.Count;)
            {
                skillEffectValues[effect.effectTo[i]] = getValueToAdd(skillEffectValues[effect.effectTo[i]]);
                i++;
            }
            
            stat.effects.Add(effect);
            // TODO: set current attributes
            // TODO: event call 
        }

        public abstract void GetHit(DamageData data);
        public void DotDamaged(DamageData data, float duration)
        {
            StartCoroutine(AddDotDamage(duration));
        }
        
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


