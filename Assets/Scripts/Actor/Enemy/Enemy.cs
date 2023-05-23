using System;
using Actor.Stats;
using Core;
using Interface;
using StateMachine;
using UnityEngine;
using UnityEngine.Pool;
using Attribute = Actor.Stats.Attribute;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        protected StateMachine<Enemy> stateMachine;
        public GameObject Target => _target;
        public int Damage => (int)_currentAttributes[AttributeType.Attack].value;
        public int spawnId;
        
        internal IObjectPool<Enemy> ManagedPool;
        internal Collider2D Collider2D;
        internal Rigidbody2D Rigidbody2D;
        internal Animator EnemyAnim;

        public void SetManagedPool(IObjectPool<Enemy> pool)
        {
            ManagedPool = pool;
        }
        public override float GetAttributeValue(AttributeType type) => _currentAttributes[type].value;
        public override void AddAttributeValue(AttributeType type, float value)
        {
            _currentAttributes[type].value += value;
        }

        public override void AddEffect(Effect effect)
        {
            // TODO: effect addition
        }

        public override void DeleteEffect(EffectType type)
        {
            // TODO: effect delete
        }

        protected override void Died()
        {
            stateMachine.ChangeState<EnemyDiedState>();
        }
    }
    
    // Values or methods that other cannot use
    public partial class Enemy
    {
        private GameObject _target;
        [SerializeField] private SerializableDictionary<AttributeType, Attribute> _currentAttributes = new();
        [SerializeField] private float _currentHealthPoint;
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor<EnemyStatObject>
    {
        protected override void Awake()
        {
            base.Awake();
            
            _currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                _currentAttributes[type] = new Attribute(type, 0);
            }
            
            forwardVector = transform.forward;
            Rigidbody2D = GetComponent<Rigidbody2D>();
            EnemyAnim = GetComponent<Animator>();
            Collider2D = GetComponent<Collider2D>();
            
            _target = GameObject.Find("Player");
            // stat.attackStrategy =
            
            stateMachine = new StateMachine<Enemy>(this, new EnemyIdleState());
            stateMachine.AddState(new EnemyMoveState());
            stateMachine.AddState(new EnemyAttackState());
            stateMachine.AddState(new EnemyGetHitState());
            stateMachine.AddState(new EnemyDiedState());
        }

        protected void OnEnable()
        {
            Collider2D.enabled = true;
            stateMachine.ChangeState<EnemyIdleState>();
            
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                _currentAttributes[type].value = stat.baseAttributes[type].value;
            }
            _currentHealthPoint = stat.baseHealthPoint;
        }

        private void Update()
        {
            if (_currentHealthPoint <= 0)
                Died();
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
    }
    
    // body of others
    public partial class Enemy
    {
        public override void Affected(Effect effect)
        {
            AddEffect(effect);
            // TODO: clac current attributes
            // TODO: event call 
        }

        public override void Released(Effect effect)
        {
            DeleteEffect(effect.type);
            // TODO: clac current attributes
        }

        public override void Damaged(DamageData data)
        {
            Debug.Log( "Enemy health Lost -> " + data.Damage);
            _currentHealthPoint -= data.Damage;
            stateMachine.ChangeState<EnemyGetHitState>();
            
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.AddForce(data.KbForce, ForceMode2D.Impulse);
        }
    }
}