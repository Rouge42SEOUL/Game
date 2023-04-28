using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("gyyu_game");
    }
    
    public void EndGame()
    {
        SceneManager.LoadScene("gyyu_end");
    }

    public void StartBattle()
    {
        SceneManager.LoadScene("gyyu_battle");
    }
}
