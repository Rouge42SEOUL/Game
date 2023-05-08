using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class AttackState : State<Enemy>
    {
        public override void OnEnter()
        {
            // TODO : Start attack coroutine, attack animation
        }
        
        public override void Update()
        {
            // 플레이어가 사라지면 기본상태
            if (!_context.Target)
            {
                _stateMachine.ChangeState<IdleState>();
            }
            
            // 공격상태에서 플레이어와 거리가 1.0f 초과면 이동상태
            float dist = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
            if (dist > 1.0f)
            {
                _stateMachine.ChangeState<MoveState>();
            }
        }
    }
}
