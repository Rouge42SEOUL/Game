
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyIdleState : State<Enemy>
    {
        public override void Update()
        {
            if (_context.Target)
            {
                // 기본상태에서 플레이어와 거리가 0.5f 이하면 공격상태, 초과면 이동상태
                float dist = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
                if (dist <= 0.5f)
                {
                    _stateMachine.ChangeState<EnemyAttackState>();
                }
                else
                {   
                    _stateMachine.ChangeState<EnemyMoveState>();
                }
            }
        }
    }
}