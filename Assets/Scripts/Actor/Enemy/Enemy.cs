using System;
using System.Collections;
using Actor.Stats;
using Interface;
using StateMachine;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        protected StateMachine<Enemy> stateMachine;
        
        internal IObjectPool<Enemy> ManagedPool;
        internal Collider2D Collider2D;
        internal Rigidbody2D Rigidbody2D;
        internal Animator EnemyAnim;
        internal GameObject Target => _target;
        internal EnemyStatObject Stat => _stat;
        public SerializedDictionary<AttributeType, int> currentAttributes;
        
        protected int baseHealthPoint;
        protected int currentHealthPoint;
        
        public override void GetHit(DamageData data) => _GetHit(data);
        public void SetManagedPool(IObjectPool<Enemy> pool) => _SetManagedPool(pool);
        public void Init() => _Init();
        
    }
    
    // Values or methods that other cannot use
    public partial class Enemy
    {
        private GameObject _target;
        [SerializeField] private float attackableDistance = 0.5f;
        [SerializeField] private EnemyStatObject _stat;
        
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor
    {
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            EnemyAnim = GetComponent<Animator>();
            Collider2D = GetComponent<Collider2D>();
            
            _target = GameObject.Find("Player");
            
            stateMachine = new StateMachine<Enemy>(this, new EnemyIdleState());
            stateMachine.AddState(new EnemyMoveState());
            stateMachine.AddState(new EnemyAttackState());
            stateMachine.AddState(new EnemyGetHitState());
            stateMachine.AddState(new EnemyDiedState());
        }

        private void Update()
        {
            // TODO : if enemy health below zero, call Died()
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
        private void _GetHit(DamageData data)
        {
            Debug.Log( "Enemy health Lost -> " + data.Damage);
            stateMachine.ChangeState<EnemyGetHitState>();
            
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.AddForce(data.KbForce, ForceMode2D.Impulse);
        }

        protected override void Died()
        {
            StopAllCoroutines();
            stateMachine.ChangeState<EnemyDiedState>();
        }
        
        private void _Init()
        {
            Collider2D.enabled = true;
            stateMachine.ChangeState<EnemyIdleState>();
            
            StartCoroutine(_KillEnemy());
            
            currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                currentAttributes.Add(type, _stat.baseAttributes[type]);
            }
        }

        private IEnumerator _KillEnemy()
        {
            yield return new WaitForSeconds(5f);
            Died();
        }

        private void _SetManagedPool(IObjectPool<Enemy> pool)
        {
            ManagedPool = pool;
        }
    }
}