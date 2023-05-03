using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class IdleState : State<Enemy>
    {
        public override void Update()
        {
            if (_context.IsAttackable)
            {
                _stateMachine.ChangeState<AttackState>();
            }
            else
            {   
                _stateMachine.ChangeState<MoveState>();
            }
        }
    }
}