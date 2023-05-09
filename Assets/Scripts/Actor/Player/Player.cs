
using System;
using Actor.Stats;
using StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        protected StateMachine<Player> StateMachine;
        internal Vector2 Movement => _movement;

        public PlayerStatObject Stat
        {
            get => _stat;
            private set
            {
                _stat = value;
            }
        }

        internal bool IsMoving => _movement != Vector2.zero;
        
        
        public override void GetHit()
        {
            Debug.Log("Hit" + this.gameObject);
        }
        
        public override void GetEffect(Effect effect, Func<int, int> getValueToAdd)
        {
            _skillEffectValues[effect.effectTo] = getValueToAdd(_skillEffectValues[effect.effectTo]);
            _stat.effects.Add(effect);
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        private Vector2 _movement;
        [SerializeField] private PlayerStatObject _stat;
        [SerializeField] private SerializedDictionary<AttributeType, int> _skillEffectValues;
        [SerializeField] private SerializedDictionary<AttributeType, int> _itemEffectValues;
    }
    
    // body of MonoBehaviour
    public partial class Player : Actor
    {
        private void Awake()
        {
            // inventory on equip item += OnEquipItem;
        }

        private void Start()
        {
            StateMachine = new StateMachine<Player>(this, new PlayerIdleState());
            StateMachine.AddState(new PlayerMoveState());
            StateMachine.AddState(new PlayerAttackState());
        }

        private void Update()
        {
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
                _stat.AddAttributeValue(type, -(_itemEffectValues[type]));
            }
            // calculate stats effect
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                _stat.AddAttributeValue(type, _itemEffectValues[type]);
            }
        }

        private void OnActivateSkill()
        {
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                _stat.AddAttributeValue(type, -(_skillEffectValues[type]));
            }
            // calculate stats effect
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                _stat.AddAttributeValue(type, _skillEffectValues[type]);
            }
        }

        private void OnMovement(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }

        private void OnAutoAttack()
        {
            _stat.skills[0].UseSkill();
        }
        private void OnSkill1()
        {
            _stat.skills[1].UseSkill();
        }
        private void OnSkill2()
        {
            _stat.skills[2].UseSkill();
        }
        private void OnSkill3()
        {
            _stat.skills[3].UseSkill();
        }
        private void OnSkillUlt()
        {
            _stat.skills[4].UseSkill();
        }
    }
}
