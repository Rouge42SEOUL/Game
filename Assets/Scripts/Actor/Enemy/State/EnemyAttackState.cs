using System.Collections;
using Interface;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyAttackState : State<Enemy>
    {
        private float _beforeAttackDelay = 1.0f;
        private float _afterAttackDelay = 0.5f;
        private WaitForSeconds _waitBefore;
        private WaitForSeconds _waitAfter;

        private EnemyAttackStrategy _attackStrategy;
        private DamageData _damageData;
        private float _distanceToTarget;

        private IEnumerator _coroutine;

        public override void OnInitialized()
        {
            // TODO : calculate attack time by attack speed;
            _waitBefore = new WaitForSeconds(_beforeAttackDelay);
            _waitAfter = new WaitForSeconds(_afterAttackDelay);
            _damageData = new DamageData(_context.Damage);
            _damageData.KbForce = Vector3.zero;
            _coroutine = AttackPlayer();

            switch (_context.attackType)
            {
                case EnemyAttackType.Projectile:
                    _attackStrategy = new ProjectileAttackStrategy(_context.Target.GetComponent<Player.Player>(), ref _context, ref _context.projectile);
                    break;
                case EnemyAttackType.Collision:
                default:
                    _attackStrategy = new CollisionAttackStrategy(_context.Target.GetComponent<Player.Player>());
                    break;
            }_damageData = new DamageData(_context.Damage);
        }
        
        public override void OnEnter()
        {
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
                _distanceToTarget = Vector3.Distance(_context.Target.transform.position, _context.transform.position);
                if (_distanceToTarget > _context.attackRange)
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
                if (_distanceToTarget <= _context.attackRange)
                {
                    _damageData.Damage = _context.Damage;
                    _attackStrategy.Attack(ref _damageData);
                    yield return _waitAfter;
                }
            }
        }
    }
}
