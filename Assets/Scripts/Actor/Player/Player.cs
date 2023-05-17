using System;
using Actor.Skill;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Actor.Stats;
using StateMachine;
using Interface;
using Unity.VisualScripting;

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
            base.Awake();
        
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
            launcher.OnAttackTrigger += stat.skills[0].OnAttackTrigger;
        }
        
        private void Start()
        {
            stat.normalAttack.SetContext(this);
            foreach (var slot in stat.skills)
            {
                slot.SetContext(GetComponent<IActorContext>());
            }
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
        public override void Damaged(DamageData data)
        {
            stat.currentHealthPoint -= data.Damage;
        }

        protected override void Died()
        {
            StateMachine.ChangeState<PlayerDiedState>();
        }

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

        private void UseSkill(int index)
        {
            if (stat.skills[index] == null)
                return;
            StateMachine.ChangeState<PlayerAttackState>();
            stat.skills[index].UseSkill();
        }
    }

    // event methods
    public partial class Player
    {
        private void OnMovement(InputValue value)
        {
            Movement = value.Get<Vector2>();
            if (!Movement.Equals(Vector2.zero))
            {
                forwardVector = Movement;
            }
        }
        
        private void OnAutoAttack()
        {
            StateMachine.ChangeState<PlayerAttackState>();
            stat.normalAttack.Use();
        }

        private void OnSkill1() => UseSkill(0);
        private void OnSkill2() => UseSkill(1);
        private void OnSkill3() => UseSkill(2);
        private void OnSkillUlt() => UseSkill(3);
    }
}
