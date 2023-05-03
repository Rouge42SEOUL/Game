using UnityEngine;

namespace SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gyyu_game");
        }
        
        public void EndGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gyyu_end");
        }

        public void StartBattle()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gyyu_battle");
        }
    }
}
