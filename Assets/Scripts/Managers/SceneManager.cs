using UnityEngine;

namespace SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public static void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
        
        public static void EndGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gyyu_end");
        }

        public static void StartBattle()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gyyu_battle");
        }
    }
}
