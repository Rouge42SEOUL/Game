using System;
using System.Collections;
using Actor.Skill;
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
        [SerializeField] public SerializableDictionary<AttributeType, float> skillEffectValues;
        [SerializeField] public T stat;

        protected bool isInitialized = false;

        public GameObject attackCollider;
        public Vector2 forwardVector;
        public ProjectileLauncher launcher;
        
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
        public void Realeased(Effect effect)
        {
            throw new NotImplementedException();
        }
    }
    
    // Values or methods that other cannot use
    public abstract partial class Actor<T>
    {
        private readonly WaitForSeconds _waitForOneSeconds = new WaitForSeconds(1f);
    }
    
    // body of MonoBehaviour
    public abstract partial class Actor<T> : MonoBehaviour
    {
        protected virtual void Awake()
        {
            attackCollider = transform.GetChild(0).gameObject;
            attackCollider.gameObject.SetActive(false);
            launcher = transform.GetChild(1).GetComponent<ProjectileLauncher>();
            launcher.SetContext(gameObject);
        }

        protected virtual void OnEnable()
        {
            if (isInitialized)
                return;
            isInitialized = true;
            
            currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                currentAttributes[type] = new Attribute(type, stat.baseAttributes[type].value);
            }
        }
    }
    
    // body of others
    public abstract partial class Actor<T> : IActorContext, IDamageable, IAffected
    {
        public GameObject GameObject => gameObject;
        public GameObject AttackCollider => attackCollider;
        public Vector2 Forward => forwardVector;
        public Vector3 Position => transform.position;
        public ProjectileLauncher Launcher => launcher;
        
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

        public abstract void Damaged(DamageData data);
        public void GetHit(DamageData data)
        {
            throw new NotImplementedException();
        }

        public void DotDamaged(DamageData damage, float duration)
        {
            StartCoroutine(AddDotDamage(damage, duration));
        }
        
        private IEnumerator AddDotDamage(DamageData damage, float duration)
        {
            while (duration > 0)
            {
                Damaged(damage);
                duration -= Time.deltaTime;
                yield return _waitForOneSeconds;
            }
        }
    }
}

