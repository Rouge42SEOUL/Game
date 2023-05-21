using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Actor.Stats;
using Core;
using Elemental;
using StateMachine;
using Interface;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        internal Vector2 Movement;

        internal Animator PlayerAnim;
        internal Rigidbody2D PlayerRigid;

        public override float GetAttributeValue(AttributeType type) => stat.currentAttributes[type].value;
        public override void AddAttributeValue(AttributeType type, float value) => stat.AddAttribute(type, value);
        public override void AddEffect(Effect effect) => stat.AddEffect(effect);
        public override void DeleteEffect(EffectType type) => stat.DeleteEffect(type);

        protected override void Died()
        {
            StateMachine.ChangeState<PlayerDiedState>();
        }
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        private Vector2 _movement;
        private PlayerInput _playerInput;
        
        private StateMachine<Player> StateMachine;
        [SerializeField] private SerializableDictionary<AttributeType, float> _itemEffectedValues;

        private void UseSkill(int index)
        {
            if (stat.skills[index] == null)
                return;
            StateMachine.ChangeState<PlayerAttackState>();
            stat.skills[index].UseSkill();
        }
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

        protected void OnEnable()
        {
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
        public override void Affected(Effect effect)
        {
            foreach (var type in effect.effectTo)
            {
                _skillEffectedValues[type] += effect.GetValueToAdd(GetAttributeValue(type));
            }
            AddEffect(effect);
            // TODO: set current attributes
            // TODO: event call 
        }

        public override void Released(Effect effect)
        {
            throw new NotImplementedException();
        }
        
        public override void Damaged(DamageData data)
        {
            stat.currentHealthPoint -= ElementalBalancer.ApplyBalance(data.ElementalType, stat.elementalType, data.Damage);
            Effect effect = null;
            ElementalBalancer.ApplyElementalEffect(data.ElementalType, ref effect);
            if (effect != null)
                Affected(effect);
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