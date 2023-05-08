
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class DiedState : State<Enemy>
    {
        public override void OnEnter()
        {
            Debug.Log("Enemy dead");
            _context.Collider2D.enabled = false;
            _context.Rigidbody2D.velocity = Vector2.zero;
            _context.EnemyAnim.SetBool(Animator.StringToHash("isDead"), true);
            _context.StartCoroutine(_EnemyDead());
        }

        public override void Update()
        {
            
        }

        private IEnumerator _EnemyDead()
        {
            yield return new WaitForSeconds(1f);
            _context.EnemyAnim.SetBool(Animator.StringToHash("isDead"), false);
            _context.ManagedPool.Release(_context);
        }
    }
}
