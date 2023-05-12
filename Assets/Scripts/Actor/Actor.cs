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
        [SerializeField] protected static SerializableDictionary<AttributeType, float> skillEffectValues;
        [SerializeField] protected static T stat;
        
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

        public virtual void GetHit(DamageData data)
        {
            switch (data.elementalType)
            {
                case ElementalType.Fire:
                {
                    if (stat.elementalType == ElementalType.Wind)
                        data.damage *= 2;
                    var effect = new Effect(EffectType.Burns, 10, 5, false);
                    AffectedConfirm(effect);
                    break;
                }
                case ElementalType.Ice :
                {
                    if (stat.elementalType == ElementalType.Fire)
                        data.damage *= 2;
                    var effect = new Effect(EffectType.Frostbite, 10, 5, false);
                    AffectedConfirm(effect);
                    break;
                }
                case ElementalType.Ground :
                {
                    if (stat.elementalType == ElementalType.Ice)
                        data.damage *= 2;
                    var effect = new Effect(EffectType.Fracture, 5, false);
                    AffectedConfirm(effect);
                    break;
                }
                case ElementalType.Wind :
                {
                    if (stat.elementalType == ElementalType.Ground)
                        data.damage *= 2;
                    var effect = new Effect(EffectType.Bleeding, 5, false);
                    AffectedConfirm(effect);
                    break;
                }
                case ElementalType.Holy :
                {
                    if (stat.elementalType == ElementalType.Dark)
                        data.damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5, false);
                    AffectedConfirm(effect);
                    break;
                }
                case ElementalType.Dark :
                {
                    if (stat.elementalType == ElementalType.Holy)
                        data.damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5, false);
                    AffectedConfirm(effect);
                    break;
                }
            }
            //TODO: get_damage
        }

        private static void AffectedConfirm(Effect effect)
        {
            var rand = new Unity.Mathematics.Random();
            var randDouble = rand.NextDouble();
            if (effect.type == EffectType.Bleeding || effect.type == EffectType.Fracture)
            {
                if(randDouble < 0.01f)
                    Affected(effect);
            }
            else
            {
                if(randDouble < 0.1f)
                    Affected(effect);
            }
        }
        private static void Affected(Effect effect)
        {
            skillEffectValues[effect.effectTo[0]] = effect.effectValue;
            stat.effects.Add(effect);
        }

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


