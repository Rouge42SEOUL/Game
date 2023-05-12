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
        public override void GetHit(DamageData data) => _GetHit(data);

        internal Vector2 Movement;
        internal Vector2 Stareing;

        internal Animator PlayerAnim;
        internal Rigidbody2D PlayerRigid;
        internal GameObject PlayerAttackCol;

        public PlayerStatObject Stat => stat;

        public override void GetEffect(Effect effect, Func<float, float> getValueToAdd)
        {
            _skillEffectValues[effect.effectTo] = getValueToAdd(_skillEffectValues[effect.effectTo]);
            stat.effects.Add(effect);
            //event call 
        }
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        private Vector2 _movement;
        private PlayerInput _playerInput;
        
        [SerializeField] private SerializedDictionary<AttributeType, float> _skillEffectValues;
        [SerializeField] private SerializedDictionary<AttributeType, float> _itemEffectValues;
    }
    
    // body of MonoBehaviour
    public partial class Player : Actor<PlayerStatObject>
    {
        private void Awake()
        {
            // inventory on equip item += OnEquipItem;
        
            PlayerAnim = GetComponent<Animator>();
            PlayerRigid = GetComponent<Rigidbody2D>();
            PlayerAttackCol = transform.GetChild(0).gameObject;
            PlayerAttackCol.gameObject.SetActive(false);
            
            StateMachine = new StateMachine<Player>(this, new PlayerIdleState());
            StateMachine.AddState(new PlayerMoveState());
            StateMachine.AddState(new PlayerAttackState());
            StateMachine.AddState(new PlayerDiedState());
        }

        private void Update()
        {
            // TODO : if player health below zero, call Died()
            StateMachine.Update();
        }
        
        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
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
        
        private void OnActivateSkill()
        {
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                AddAttributeValue(type, -(_skillEffectValues[type]));
            }
            // calculate stats effect
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                AddAttributeValue(type, _skillEffectValues[type]);
            }
        }

        private void _GetHit(DamageData data)
        {
            // TODO : get damaged, remove Debug.Log
            Debug.Log("Player health lost ->" + data.Damage);
        }

        protected override void Died()
        {
            Debug.Log("Player Died");
            StopAllCoroutines();
            StateMachine.ChangeState<PlayerDiedState>();
        }

        private void OnMovement(InputValue value)
        {
            Movement = value.Get<Vector2>();
            if (!Movement.Equals(Vector2.zero))
            {
                Stareing = Movement;
            }
        }

        private void OnAutoAttack(InputValue value)
        {
            StateMachine.ChangeState<PlayerAttackState>();
            stat.attack.Use();
        }
        private void OnSkill1()
        {
            stat.skills[0].UseSkill();
            // TODO : Remove hardcoded death
            StateMachine.ChangeState<PlayerDiedState>();
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
