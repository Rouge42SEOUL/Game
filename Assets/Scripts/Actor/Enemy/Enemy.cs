using Actor.Stats;
using StateMachine;
using UnityEngine;
using UnityEngine.Rendering;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        protected StateMachine<Enemy> stateMachine;
        
        internal GameObject Target => _target;
        internal EnemyStatObject Stat => _stat;
        public SerializedDictionary<AttributeType, int> CurrentAttributes { get; protected set; }
        
        protected int baseHealthPoint;
        protected int currentHealthPoint;
        
        public override void GetHit() => _GetHit();

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
        private EnemyStatObject _stat;
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor
    {
        private void OnEnable()
        {
            CurrentAttributes[AttributeType.Health] = _stat.baseAttributes[AttributeType.Health];
            CurrentAttributes[AttributeType.Attack] = _stat.baseAttributes[AttributeType.Attack];
            CurrentAttributes[AttributeType.Speed] = _stat.baseAttributes[AttributeType.Speed];
        }

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
    }
}