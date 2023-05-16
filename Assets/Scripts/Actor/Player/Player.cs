using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Actor.Stats;
using StateMachine;
using Interface;


namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        private StateMachine<Player> _stateMachine;
        public override void GetHit(DamageData data) => _GetHit(data);

        internal Vector2 Movement;

        internal Animator PlayerAnim;
        internal Rigidbody2D PlayerRigid;

        public PlayerStatObject Stat => stat;
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        private Vector2 _movement;
        private PlayerInput _playerInput;
        
        [SerializeField] private SerializedDictionary<AttributeType, float> itemEffectValues;
    }
    
    // body of MonoBehaviour
    public partial class Player : Actor<PlayerStatObject>
    {
        protected override void Awake()
        {
            stat.normalAttack.context = this;
            // inventory on equip item += OnEquipItem;
        
            PlayerAnim = GetComponent<Animator>();
            PlayerRigid = GetComponent<Rigidbody2D>();
            
            _stateMachine = new StateMachine<Player>(this, new PlayerIdleState());
            _stateMachine.AddState(new PlayerMoveState());
            _stateMachine.AddState(new PlayerAttackState());
            _stateMachine.AddState(new PlayerDiedState());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            attackCollider.GetComponent<PlayerAttackCol>().OnAttackTrigger += stat.normalAttack.OnAttackTrigger;
        }
        
        private void Start()
        {
            Debug.Log(this.GetType() + " vs " + stat.normalAttack.context.GetType());
            stat.normalAttack.context = this;
        }

        private void Update()
        {
            // TODO : if player health below zero, call Died()
            _stateMachine.Update();
        }
        
        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void OnDisable()
        {
            attackCollider.GetComponent<PlayerAttackCol>().OnAttackTrigger -= stat.normalAttack.OnAttackTrigger;
        }
    }
    
    // body of others
    public partial class Player
    {
        private void OnEquipItem()
        {
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                AddAttributeValue(type, -(itemEffectValues[type]));
            }

            // calculate stats effect
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                AddAttributeValue(type, itemEffectValues[type]);
            }
        }

        private void _GetHit(DamageData data)
        {
            // TODO : get damaged, remove Debug.Log
            Debug.Log("Player health lost ->" + data.damage);
        }

        protected override void Died()
        {
            Debug.Log("Player Died");
            StopAllCoroutines();
            _stateMachine.ChangeState<PlayerDiedState>();
        }

        private void OnMovement(InputValue value)
        {
            Movement = value.Get<Vector2>();
            if (!Movement.Equals(Vector2.zero))
            {
                forwardVector = Movement;
            }
        }

        private void OnAutoAttack(InputValue value)
        {
            _stateMachine.ChangeState<PlayerAttackState>();
            stat.normalAttack.Use();
        }
        private void OnSkill1()
        {
            // TODO : Remove hardcoded death
            _stateMachine.ChangeState<PlayerDiedState>();
            stat.skills[0].UseSkill();
        }
        private void OnSkill2()
        {
            stat.skills[1].UseSkill();
        }
        private void OnSkill3()
        {
            stat.skills[2].UseSkill();
        }
        private void OnSkillUlt()
        {
            stat.skills[3].UseSkill();
        }
    }
}
