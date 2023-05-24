
using TMPro;
using UnityEngine;

namespace Managers.MainScene
{
    public partial class ConfirmPopupManager
    {
        
    }
    
    public partial class ConfirmPopupManager
    {
        [SerializeField] private TMP_Text text;
    }
    
    public partial class ConfirmPopupManager : MonoBehaviour
    {
        private void OnEnable()
        {
            text.text = TitleManager.Instance.PassiveList[TitleManager.Instance.selectedPassiveId].PassiveName
                        + " " + TitleManager.Instance.ClassList[TitleManager.Instance.selectedClassId].ClassName;
        }
    }
    
    public partial class ConfirmPopupManager
    {
        
    }
}

