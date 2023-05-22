
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerAttackState : State<Player>
    {
        public override void OnEnter()
        {
            _context.PlayerAnim.SetBool(Animator.StringToHash("isAttacking"), true);
            _context.StartCoroutine(_AttackSpeed());
        }
        
        // Update is called once per frame
        public override void Update()
        {
            
        }

        public override void OnExit()
        {
            _context.attackCollider.SetActive(false);
            _context.PlayerAnim.SetBool(Animator.StringToHash("isAttacking"), false);
        }

        private void _Finish()
        {
            _stateMachine.ChangeState<PlayerIdleState>();
        }
        
        private IEnumerator _AttackSpeed()
        {
            yield return new WaitForSeconds(0.1f);
            _context.attackCollider.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            _Finish();
        }

    }
}
