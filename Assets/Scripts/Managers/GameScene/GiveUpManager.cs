
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.GameScene
{
    public class GiveUpManager : MonoBehaviour
    {
        #region PrivateVariable

        [SerializeField] private GameObject giveUpPanel;

        #endregion

        #region PublicMethod

        public void ToMainScene()
        {
            DataManager.DataManager.Instance.DeleteData();
            SceneManager.LoadScene("MainScene");
        }

        #endregion

        #region MonoMethod

        private void Awake()
        {
            giveUpPanel.SetActive(false);
        }

        #endregion

        #region PrivateMethod

        private void OnGiveUp()
        {
            giveUpPanel.SetActive(!giveUpPanel.activeSelf);
        }

        #endregion
    }
}

