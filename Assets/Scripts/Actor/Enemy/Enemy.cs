using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Stats;
using StateMachine;
using UnityEngine;
using UnityEngine.Pool;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        protected StateMachine<Enemy> stateMachine;
        internal GameObject Target => _target;
        internal EnemyStatObject Stat => _stat;
        public Dictionary<AttributeType, int> currentAttributes = new();
        
        protected int baseHealthPoint;
        protected int currentHealthPoint;
        
        public override void GetHit() => _GetHit();
        public void SetManagedPool(IObjectPool<Enemy> pool) => _SetManagedPool(pool);
        public void Init() => _Init();

        internal bool IsAttackable
        {
            get
            {
                float dis = Vector2.Distance(_target.transform.position, transform.position);
                return attackableDistance >= dis;
            }
        }
    }
    
    // Values or methods that other cannot use
    public partial class Enemy
    {
        private GameObject _target;
        [SerializeField] private float attackableDistance = 0.5f;
        [SerializeField] private EnemyStatObject _stat;
        private IObjectPool<Enemy> _managedPool;
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor
    {
        private void Start()
        {
            _target = GameObject.Find("Player");
            stateMachine = new StateMachine<Enemy>(this, new IdleState());
            stateMachine.AddState(new MoveState());
            stateMachine.AddState(new AttackState());
        }

        private void Update()
        {
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
        private void _GetHit()
        {
            throw new System.NotImplementedException();
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }
        
        private void _Init()
        {
            StartCoroutine(_KillEnemy());
            currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                currentAttributes[type] = _stat.baseAttributes[(int)type];
            }
        }

        private IEnumerator _KillEnemy()
        {
            yield return new WaitForSeconds(5f);
            _DestroyEnemy();
        }
        
        private void _DestroyEnemy()
        {
            _managedPool.Release(this);
        }

        private void _SetManagedPool(IObjectPool<Enemy> pool)
        {
            _managedPool = pool;
        }
        
        public int GetAttributeValue(AttributeType type)
        {
            return currentAttributes[type];
        }
    }
}