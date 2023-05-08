
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerAttackState : State<Player>
    {
        private Transform _atkPos;
        private Collider2D _atkCol;
        
        public override void OnInitialized()
        {
            _atkPos = _context.PlayerAttackCol.transform;
            _atkCol = _context.PlayerAttackCol.GetComponent<BoxCollider2D>();
        }
        
        public override void OnEnter()
        {
            _context.PlayerAnim.SetBool(Animator.StringToHash("isAttacking"), true);
            
            _context.PlayerAttackCol.SetActive(true);
            _SetAttackCol();
            
            _context.StartCoroutine(_AttackSpeed());
        }
        
        // Update is called once per frame
        public override void Update()
        {
            
        }

        public override void OnExit()
        {
            _context.PlayerAnim.SetBool(Animator.StringToHash("isAttacking"), false);
        }

        private void _Finish()
        {
            _stateMachine.ChangeState<PlayerIdleState>();
        }
        
        private IEnumerator _AttackSpeed()
        {
            yield return new WaitForSeconds(0.1f);
            _context.PlayerAttackCol.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            _Finish();
        }

        private void _SetAttackCol()
        {
            Vector2 t = new Vector2(Mathf.Abs(_context.Stareing.y), Mathf.Abs(_context.Stareing.x));
            _atkPos.localScale = t * 0.5f + new Vector2(1, 1);;
            _atkPos.localPosition = _context.Stareing * 0.5f;
        }

        private void _GiveDamage()
        {
            
        }
    }
}
