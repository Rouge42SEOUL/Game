
using System.Collections.Generic;
using UnityEngine;

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

        public int selectedClassId;
        
        public List<CharClassData> ClassList;

        public void QuitGame() => _QuitGame();


        public GameObject confirmPopup;
    }

    public partial class TitleManager
    {
        
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
            //TODO : get class Id data and send to GameScene
        }
    }
}

