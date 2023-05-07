
using System.Linq;
using Actor.Stats;
using UnityEngine;
using StateMachine;

namespace Actor.Player
{
    public class PlayerMoveState : State<Player>
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _playerPos;

        private float Speed => _context.Stat.GetAttributeValue(AttributeType.Speed);

        public override void OnInitialized()
        {
            _rigidbody2D = _context.GetComponent<Rigidbody2D>();
        }
        
        // Update is called once per frame
        public override void Update()
        {
            if (!_context.IsMoving)
            {
                _stateMachine.ChangeState<PlayerIdleState>();
            }
        }

        public override void FixedUpdate()
        {
            if (_context.IsMoving)
            {
                _rigidbody2D.MovePosition(_rigidbody2D.position + Speed * Time.fixedDeltaTime * _context.Movement);
            }
        }
    }
}
