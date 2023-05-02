using System;
using System.Collections;
using StateMachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.UI;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        protected StateMachine<Enemy> stateMachine;
        public override void GetHit() => _GetHit();
        public void SetManagedPool(IObjectPool<Enemy> pool) => _SetManagedPool(pool);

        public void Init() => _Init();
        
        internal GameObject Target => _target;
        internal ActorStatObject Stat => stat;

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
    }
}