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
        protected StateMachine<Player> StateMachine;

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
        
        [SerializeField] private SerializedDictionary<AttributeType, float> _itemEffectValues;
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
            
            StateMachine = new StateMachine<Player>(this, new PlayerIdleState());
            StateMachine.AddState(new PlayerMoveState());
            StateMachine.AddState(new PlayerAttackState());
            StateMachine.AddState(new PlayerDiedState());
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
            if (stat.currentHealthPoint <= 0)
                Died();
            StateMachine.Update();
        }
        
        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
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
                AddAttributeValue(type, -(_itemEffectValues[type]));
            }

            // calculate stats effect
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                AddAttributeValue(type, _itemEffectValues[type]);
            }
        }

        public override void Damaged(DamageData data)
        {
            stat.currentHealthPoint -= data.damage;
        }

        protected override void Died()
        {
            StateMachine.ChangeState<PlayerDiedState>();
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
            StateMachine.ChangeState<PlayerAttackState>();
            stat.normalAttack.Use();
        }
        
        private void OnSkill1()
        {
            // TODO : Remove hardcoded death
            StateMachine.ChangeState<PlayerDiedState>();
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