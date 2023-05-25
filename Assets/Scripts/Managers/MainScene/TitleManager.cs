
using System.Collections.Generic;
using Actor.Stats;
using Skill;

using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton Manager, Only used in Main Scene and Only this scripts run in Main Scene
namespace Managers.MainScene
{
    public struct CharClassData
    {
        public bool IsOpened;
        public int Id;
        public string ClassName;
        public string ImageSourcePath;

        public CharClassData(bool isOpened, int id, string className, string imageSourcePath = "")
        {
            IsOpened = isOpened;
            Id = id;
            ClassName = className;
            ImageSourcePath = imageSourcePath;
        }
        
        public CharClassData(CharClassData data)
        {
            IsOpened = data.IsOpened;
            Id = data.Id;
            ClassName = data.ClassName;
            ImageSourcePath = data.ImageSourcePath;
        }
    }
    
    public struct PassiveData
    {
        public int Id;
        public string PassiveName;

        public PassiveData(int id, string passiveName)
        {
            Id = id;
            PassiveName = passiveName;
        }
        
        public PassiveData(PassiveData data)
        {
            Id = data.Id;
            PassiveName = data.PassiveName;
        }
    }
    
    public partial class TitleManager
    {
        private static TitleManager _instance;
        
        public static TitleManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    return null;
                }
                return _instance;
            }
        }

        [HideInInspector]public int selectedClassId;
        [HideInInspector]public int selectedPassiveId;
        
        public List<CharClassData> ClassList;
        public List<PassiveData> PassiveList;

        public void QuitGame() => _QuitGame();
        public void ToGameScene() => _ToGameScene();

        public void NewGame() => _NewGame();
        public void LoadGame() => _LoadGame();
        public void StartNewGame() => _StartNewGame();

        public GameObject confirmPopup;
        public GameObject passiveSelect;
        public GameObject classSelect;
        public GameObject newGamePopUp;

        public void SelectPassive(int n) => _SelectPassive(n);
    }

    public partial class TitleManager
    {
        [SerializeField] private PlayerStatObject playerStat;
        [SerializeField] private List<PassiveSkillObject> passive;
    }
    
    public partial class TitleManager : MonoBehaviour
    {
        private void Awake()
        {
            if (null == _instance)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            selectedClassId = -1;
            
            // TODO : get List from serialized data
            // TODO : establish a rule about class id
            ClassList = new List<CharClassData>();
            ClassList.Add(new CharClassData(true,0, "Warrior", "Illustration/Character/warrior"));
            ClassList.Add(new CharClassData(false,-1, "Unlocked", "Illustration/Character/locked_1"));
            ClassList.Add(new CharClassData(false,-1, "Unlocked", "Illustration/Character/locked_2"));
            ClassList.Add(new CharClassData(false,-1, "Unlocked", "Illustration/Character/locked_3"));

            PassiveList = new List<PassiveData>();
            PassiveList.Add(new PassiveData(0, "Ice"));
            PassiveList.Add(new PassiveData(1, "Holy"));
            PassiveList.Add(new PassiveData(2, "Wind"));
            PassiveList.Add(new PassiveData(3, "Fire"));
            PassiveList.Add(new PassiveData(4, "Dark"));
            PassiveList.Add(new PassiveData(5, "Ground"));
        }
    }
    
    public partial class TitleManager
    {
        private void _QuitGame()
        {
            Application.Quit(0);
        }

        private void _ToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void _SelectPassive(int n)
        {
            playerStat.passive = passive[n];
        }

        private void _NewGame()
        {
            if (DataManager.DataManager.Instance.HasData())
            {
                newGamePopUp.SetActive(true);
            }
            else
            {
                classSelect.SetActive(true);
            }
        }

        private void _StartNewGame()
        {
            GameManager.Instance.InitGame();
        }

        private void _LoadGame()
        {
            if (DataManager.DataManager.Instance.HasData())
            {
                DataManager.DataManager.Instance.LoadData();
                _ToGameScene();
            }
        }
        
        //private void 
    }
}

