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
        private Rigidbody2D _rigidbody2D;
        private Transform _playerPos;
        private int _speed => _context.currentAttributes[(int)AttributeType.Speed];

        public override void OnInitialized()
        {
            _rigidbody2D = _context.GetComponent<Rigidbody2D>();
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
                _rigidbody2D.MovePosition(_rigidbody2D.position + _speed * Time.fixedDeltaTime * pos);
            }
            else
            {
                _stateMachine.ChangeState<AttackState>();
            }
        }
    }

}
