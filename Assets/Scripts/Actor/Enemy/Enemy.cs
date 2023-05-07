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
        
        internal Rigidbody2D Rigidbody2D;
        internal GameObject Target => _target;
        internal EnemyStatObject Stat => _stat;
        public SerializedDictionary<AttributeType, int> currentAttributes;
        
        protected int baseHealthPoint;
        protected int currentHealthPoint;
        
        public override void GetHit(DamageData data) => _GetHit(data);
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
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _target = GameObject.Find("Player");
        }
        
        private void Start()
        {
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

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     // TODO : Call _GetHit function of Player
        //     
        //     throw new System.NotImplementedException();
        // }
    }
    
    // body of others
    public partial class Enemy
    {
        private void _GetHit(DamageData data)
        {
            Debug.Log(data.Damage + "health Lost");
            // TODO : make GetHitState of Enemy and put Addforce func in it
            Rigidbody2D.AddForce(data.KbForce, ForceMode2D.Impulse);
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
            // TODO : make 
        }
        
        private void _Init()
        {
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