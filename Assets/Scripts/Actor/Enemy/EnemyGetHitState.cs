
using System.Collections;
using StateMachine;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyGetHitState : State<Enemy>
    {
        private float _stunTime = 1.0f;
        private float _temp;
        
        public override void OnInitialized()
        {
        }

        public override void OnEnter()
        {
            _context.EnemyAnim.SetBool(Animator.StringToHash("getHit"), true);
            _temp = 0f;
        }
            
        public override void Update()
        {
            _temp += Time.deltaTime;
            if (_temp > _stunTime)
            {
                _Finish();
            }
        }

        public override void OnExit()
        {
            _context.Rigidbody2D.velocity = Vector2.zero;
            _context.EnemyAnim.SetBool(Animator.StringToHash("getHit"), false);
        }
        
        private void _Finish()
        {
            _stateMachine.ChangeState<EnemyIdleState>();
        }
    }
}
