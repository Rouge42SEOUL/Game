
using System;
using Actor.Stats;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        protected StateMachine<Player> StateMachine;
        public override void GetHit() => _GetHit();
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
        
        private void _GetHit()
        {
            throw new System.NotImplementedException();
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }

        private void OnMovement(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
    }
}
