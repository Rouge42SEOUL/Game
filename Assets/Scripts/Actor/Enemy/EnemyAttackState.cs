using System.Collections;
using Interface;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyAttackState : State<Enemy>
    {
        private Player.Player _p;
        
        private float _attackTime = 0.5f;
        private float _attackAfterTime = 1.0f;

        // TODO : Change data by Stat
        private DamageData _tempData;
        
        public override void OnInitialized()
        {
            // TODO : calculate attack time by attack speed;
            _p = _context.Target.GetComponent<Player.Player>();
            _tempData.Damage = 5;
            _tempData.KbForce = Vector3.zero;
        }
        
        public override void OnEnter()
        {
            // TODO : Start attack coroutine, attack animation
            _context.EnemyAnim.SetBool(Animator.StringToHash("isAttacking"), true);
            _context.StartCoroutine(AttackPlayer());
        }
        
        public override void Update()
        {
            // 플레이어가 사라지면 기본상태
            if (!_context.Target.activeSelf)
            {
                _stateMachine.ChangeState<EnemyIdleState>();
            }
            else
            {
                // 공격상태에서 플레이어와 거리가 1.0f 초과면 이동상태
                float dist = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
                if (dist > 1.0f)
                {
                    _stateMachine.ChangeState<EnemyMoveState>();
                }
            }
        }

        public override void OnExit() 
        {
            _context.StopCoroutine(AttackPlayer());
            _context.EnemyAnim.SetBool(Animator.StringToHash("isAttacking"), false);
        }

        private IEnumerator AttackPlayer()
        {
            yield return new WaitForSeconds(_attackTime);
            _p.GetHit(_tempData);
            yield return new WaitForSeconds(_attackAfterTime);
        }
    }
}
