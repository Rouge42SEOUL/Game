
using StateMachine;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerIdleState : State<Player>
    {
        // Update is called once per frame
        public override void Update()
        {
            if (!_context.Movement.Equals(Vector2.zero))
            {
                _stateMachine.ChangeState<PlayerMoveState>();
            }
        }
    }
}

