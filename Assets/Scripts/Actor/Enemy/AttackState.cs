using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class AttackState : State<Enemy>
    {
        public override void Update()
        {
            if (!_context.IsAttackable)
            {
                _stateMachine.ChangeState<MoveState>();
            }
        }
    }
}
