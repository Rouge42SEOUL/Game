using System.Collections;
using System.Collections.Generic;
using Actor.Enemy;
using Actor.Stats;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class MoveState : State<Enemy>
    {
        private Transform _playerPos;
        private int _speed => _context.currentAttributes[AttributeType.Speed];

        public override void OnInitialized()
        {
            _playerPos = _context.Target.transform;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            if (!_context.IsAttackable)
            {
                Vector2 pos = Vector3.Normalize(_playerPos.position - _context.transform.position);
                _context.Rigidbody2D.MovePosition(_context.Rigidbody2D.position + _speed * Time.fixedDeltaTime * pos);
            }
            else
            {
                _stateMachine.ChangeState<AttackState>();
            }
        }
    }

}
