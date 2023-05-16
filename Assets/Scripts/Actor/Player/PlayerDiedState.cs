
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerDiedState : State<Player>
    {
        public override void OnEnter()
        {
            _context.PlayerAnim.SetBool(Animator.StringToHash("isDead"), true);
            _context.StartCoroutine(_PlayerDead());
        }

        public override void Update()
        {
            
        }

        private IEnumerator _PlayerDead()
        {
            yield return new WaitForSeconds(3f);
            _context.gameObject.SetActive(false);
        }
    }
}
