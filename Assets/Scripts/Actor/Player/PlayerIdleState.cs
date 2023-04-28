
using StateMachine;

namespace Actor.Player
{
    public class PlayerIdleState : State<Player>
    {
        // Update is called once per frame
        public override void Update()
        {
            if (_context.IsMoving)
            {
                _stateMachine.ChangeState<PlayerMoveState>();
            }
            else
            {
                _stateMachine.ChangeState<PlayerIdleState>();
            }
        }
    }
}

