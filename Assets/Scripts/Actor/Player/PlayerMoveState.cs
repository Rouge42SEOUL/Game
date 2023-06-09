
using Actor.Stats;
using UnityEngine;
using StateMachine;

namespace Actor.Player
{
    public class PlayerMoveState : State<Player>
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _playerPos;

        private float Speed => _context.GetAttributeValue(AttributeType.MoveSpeed);
        private int _moveXid;
        private int _moveYid;

        public override void OnInitialized()
        {
            _moveXid = Animator.StringToHash("moveX");
            _moveYid = Animator.StringToHash("moveY");
        }

        public override void OnEnter()
        {
            _context.PlayerAnim.SetBool(Animator.StringToHash("isMoving"), true);
        }
        
        // Update is called once per frame
        public override void Update()
        {
            if (_context.Movement == Vector2.zero)
            {
                _stateMachine.ChangeState<PlayerIdleState>();
            }
            else
            {
                _context.PlayerAnim.SetFloat(_moveXid, _context.Forward.x);
                _context.PlayerAnim.SetFloat(_moveYid, _context.Forward.y);
            }
        }

        public override void FixedUpdate()
        {
            _context.PlayerRigid.MovePosition(_context.PlayerRigid.position + Speed * Time.fixedDeltaTime * _context.Movement);
        }
        
        public override void OnExit()
        {
            _context.PlayerAnim.SetBool(Animator.StringToHash("isMoving"), false);
        }
    }
}
