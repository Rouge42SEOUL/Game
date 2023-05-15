using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Skill;
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
        public GameObject Target => _target;
        
        internal IObjectPool<Enemy> ManagedPool;
        internal Collider2D Collider2D;
        internal Rigidbody2D Rigidbody2D;
        internal Animator EnemyAnim;

        public void SetManagedPool(IObjectPool<Enemy> pool) => _SetManagedPool(pool);
    }
    
    // Values or methods that other cannot use
    public partial class Enemy
    {
        private GameObject _target;
        [SerializeField] private int _currentHealthPoint;
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor<EnemyStatObject>
    {
        protected override void Awake()
        {
            forwardVector = transform.forward;
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

        protected override void OnEnable()
        {
            base.OnEnable();
            _Init();
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
        public override void Damaged(DamageData data)
        {
            Debug.Log( "Enemy health Lost -> " + data.Damage);
            _currentHealthPoint -= data.Damage;
            stateMachine.ChangeState<EnemyGetHitState>();
            
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.AddForce(data.KbForce, ForceMode2D.Impulse);
        }

        protected override void Died()
        {
            stateMachine.ChangeState<EnemyDiedState>();
        }
        
        private void _Init()
        {
            Collider2D.enabled = true;
            stateMachine.ChangeState<EnemyIdleState>();
            
            foreach (var att in currentAttributes)
            {
                att.Value.value = stat.baseAttributes[att.Key].value;
            }
            _currentHealthPoint = stat.baseHealthPoint;
        }

        private void _SetManagedPool(IObjectPool<Enemy> pool)
        {
            ManagedPool = pool;
        }
    }
}