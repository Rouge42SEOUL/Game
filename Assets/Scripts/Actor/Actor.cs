using System;
using System.Collections;
using Actor.Stats;
using Core;
using Elemental;
using Interface;
using Skill.Projectile;
using UnityEngine;
using Attribute = Actor.Stats.Attribute;

namespace Actor
{
    // Values or methods that other can use
    public abstract partial class Actor<T> where T : ActorStatObject
    {
        [SerializeField] protected T stat;
        [SerializeField] protected SerializableDictionary<AttributeType, float> _skillEffectedValues = new();

        protected bool isInitialized = false;

        protected Vector2 forwardVector;
        protected GameObject attackCollider;
        protected ProjectileLauncher launcher;
        
        public Action OnHPChanged;
        public abstract void AddHP(float value);
        protected abstract void CheckDied();

        public abstract float GetAttributeValue(AttributeType type);
        public abstract void AddAttributeValue(AttributeType type, float value);
        public abstract void AddEffect(Effect effect);
        public abstract void DeleteEffect(EffectType type);
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
            if (isInitialized)
                return;
            isInitialized = true;
            
            attackCollider = transform.GetChild(0).gameObject;
            attackCollider.gameObject.SetActive(false);
            launcher = transform.GetChild(1).GetComponent<ProjectileLauncher>();
            launcher.SetContext(gameObject);
        }

        protected virtual void OnEnable()
        {
            OnHPChanged += CheckDied;
        }

        protected virtual void OnDisable()
        {
            OnHPChanged -= CheckDied;
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

        public abstract void Affected(Effect effect);
        public abstract void Released(Effect effect);
        public abstract void Damaged(DamageData data);
        
        public abstract bool CalculateHit(SerializableDictionary<AttributeType, Attribute> baseAttributes);
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

