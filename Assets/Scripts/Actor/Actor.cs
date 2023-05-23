using System;
using System.Collections;
using Actor.Stats;
using Core;
using Elemental;
using Interface;
using Skill.Projectile;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = Actor.Stats.Attribute;

namespace Actor
{
    // Values or methods that other can use
    public abstract partial class Actor<T> where T : ActorStatObject
    {
        [SerializeField] protected T stat;
        [SerializeField] protected SerializableDictionary<AttributeType, float> _skillEffectedValues = new();

        protected bool isInitialized = false;

        public Vector2 forwardVector;
        private GameObject _attackCollider;
        private ProjectileLauncher _launcher;
        
        protected abstract void Died();

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
            
            _attackCollider = transform.GetChild(0).gameObject;
            _attackCollider.gameObject.SetActive(false);
            _launcher = transform.GetChild(1).GetComponent<ProjectileLauncher>();
            _launcher.SetContext(gameObject);
        }
    }
    
    // body of others
    public abstract partial class Actor<T> : IActorContext, IDamageable, IAffected
    {
        public GameObject GameObject => gameObject;
        public GameObject AttackCollider => _attackCollider;
        public Vector2 Forward => forwardVector;
        public Vector3 Position => transform.position;
        public ProjectileLauncher Launcher => _launcher;

        public abstract void Affected(Effect effect);
        public abstract void Released(Effect effect);
        public abstract void Damaged(DamageData data);
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

