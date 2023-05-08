
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class GetHitState : State<Enemy>
    {
        private float _stunTime = 1.5f;
        
        public override void OnInitialized()
        {
        }

        public override void OnEnter()
        {
            _context.EnemyAnim.SetBool(Animator.StringToHash("getHit"), true);
            _context.StartCoroutine(_Hit());
        }
            
        public override void Update()
        {
        }

        public override void OnExit()
        {
            _context.StopCoroutine(_Hit());
            _context.EnemyAnim.SetBool(Animator.StringToHash("getHit"), false);
        }
        
        private void _Finish()
        {
            _stateMachine.ChangeState<IdleState>();
        }
        
        private IEnumerator _Hit()
        {
            yield return new WaitForSeconds(_stunTime);
            _Finish();
        }
    }
}
