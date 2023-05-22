using System.Collections;
using Interface;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyAttackState : State<Enemy>
    {
        private Player.Player _p;

        private float _beforeAttackDelay = 1.0f;
        private float _afterAttackDelay = 0.5f;
        private WaitForSeconds _waitBefore;
        private WaitForSeconds _waitAfter;

        private DamageData _damageData;
        private float _distanceToTarget;

        private IEnumerator _coroutine;

        public override void OnInitialized()
        {
            // TODO : calculate attack time by attack speed;
            _p = _context.Target.GetComponent<Player.Player>();
            _waitBefore = new WaitForSeconds(_beforeAttackDelay);
            _waitAfter = new WaitForSeconds(_afterAttackDelay);
            _damageData = new DamageData(_context.Damage);
            _damageData.KbForce = Vector3.zero;
            _coroutine = AttackPlayer();
        }
        
        public override void OnEnter()
        {
            Debug.Log("Enemy Attack Started");
            _distanceToTarget = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
            _context.EnemyAnim.SetBool(Animator.StringToHash("isAttacking"), true);
            _context.StartCoroutine(_coroutine);
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
                _distanceToTarget = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
                if (_distanceToTarget > 1.0f)
                {
                    _stateMachine.ChangeState<EnemyMoveState>();
                }
            }
        }

        public override void OnExit() 
        {
            _context.StopCoroutine(_coroutine);
            _context.EnemyAnim.SetBool(Animator.StringToHash("isAttacking"), false);
        }

        private IEnumerator AttackPlayer()
        {
            while (true)
            {
                yield return _waitBefore;
                if (_distanceToTarget <= 1.0f)
                {
                    _damageData.Damage = _context.Damage;
                    _p.Damaged(_damageData);
                    yield return _waitAfter;
                }
            }
        }
    }
}
