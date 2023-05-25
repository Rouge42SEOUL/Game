
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyIdleState : State<Enemy>
    {
        private float dist;
        
        public override void Update()
        {
            if (_context.Target)
            {
                dist = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
                if (dist <= _context.attackRange - 0.5f)
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