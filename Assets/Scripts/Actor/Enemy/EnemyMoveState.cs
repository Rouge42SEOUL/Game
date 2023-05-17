using System.Collections;
using System.Collections.Generic;
using Actor.Enemy;
using Actor.Stats;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyMoveState : State<Enemy>
    {
        private Transform _playerPos;
        private float _speed => _context.currentAttributes[AttributeType.MoveSpeed].value;
        
        private int _moveXid;
        private int _moveYid;
    
        private Vector2 pos;

        public override void OnInitialized()
        {
            _playerPos = _context.Target.transform;
            _moveXid = Animator.StringToHash("moveX");
            _moveYid = Animator.StringToHash("moveY");
        }

        public override void OnEnter()
        {
            _context.EnemyAnim.SetBool(Animator.StringToHash("isMoving"), true);
        }
        
        public override void Update()
        {
            // 플레이어가 사라지면 기본상태
            if (!_context.Target)
            {
                _stateMachine.ChangeState<EnemyIdleState>();
            }
            
            // 이동상태에서 플레이어와 거리가 0.5f 이하면 공격상태
            float dist = Vector3.Distance(_playerPos.position, _context.transform.position);
            if (dist <= 0.5f)
            {
                _stateMachine.ChangeState<EnemyAttackState>();
            }
            else
            {
                _context.EnemyAnim.SetFloat(_moveXid, pos.x);
                _context.EnemyAnim.SetFloat(_moveYid, pos.y);
            }
        }

        public override void FixedUpdate()
        {
            pos = Vector3.Normalize(_playerPos.position - _context.transform.position);
            _context.Rigidbody2D.MovePosition(_context.Rigidbody2D.position + _speed * Time.fixedDeltaTime * pos);
        }
        
        public override void OnExit()
        {
            _context.EnemyAnim.SetBool(Animator.StringToHash("isMoving"), false);
        }
    }

}
