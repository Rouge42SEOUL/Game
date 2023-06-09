
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class EnemyDiedState : State<Enemy>
    {
        private readonly WaitForSeconds _waitRealease = new WaitForSeconds(1f);
        public override void OnEnter()
        {
            _context.Collider2D.enabled = false;
            _context.Rigidbody2D.velocity = Vector2.zero;
            _context.EnemyAnim.SetBool(Animator.StringToHash("isDead"), true);
            _context.StopAllCoroutines();
            _context.StartCoroutine(_EnemyDead());
        }

        public override void Update()
        {
        }

        private IEnumerator _EnemyDead()
        {
            yield return _waitRealease;
            _context.EnemyAnim.SetBool(Animator.StringToHash("isDead"), false);
            _context.ManagedPool.Release(_context);
        }
    }
}
