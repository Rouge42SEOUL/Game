
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
            text.text = TitleManager.Instance.ClassList[TitleManager.Instance.selectedClassId - 1].ClassName;
        }
    }
    
    public partial class ConfirmPopupManager
    {
        
    }
}

