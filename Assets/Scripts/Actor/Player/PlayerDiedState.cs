
using System.Collections;
using Managers.DataManager;
using StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Actor.Player
{
    public class PlayerDiedState : State<Player>
    {
        public override void OnEnter()
        {
            Debug.Log("Player Died");
            _context.PlayerAnim.SetBool(Animator.StringToHash("isDead"), true);
            _context.StopAllCoroutines();
            _context.StartCoroutine(_PlayerDead());
        }

        public override void Update()
        {
            
        }

        private IEnumerator _PlayerDead()
        {
            yield return new WaitForSeconds(3f);
            _context.gameObject.SetActive(false);
            GameManager.Instance.InitGame();
            SceneManager.LoadScene("MainScene");
        }
    }
}
