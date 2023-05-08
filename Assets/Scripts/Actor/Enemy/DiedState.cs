
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    public class DiedState : State<Enemy>
    {
        public override void OnEnter()
        {
            _context.EnemyAnim.SetBool(Animator.StringToHash("isDead"), true);
            _context.StartCoroutine(_EnemyDead());
        }

        public override void Update()
        {
        }

        private IEnumerator _EnemyDead()
        {
            yield return new WaitForSeconds(1f);
            Object.Destroy(_context.gameObject);        }
    }
}
