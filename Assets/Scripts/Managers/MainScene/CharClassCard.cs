
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.MainScene
{
    public partial class CharClassCard
    {
        public CharClassData Data;
    }
    
    public partial class CharClassCard
    {
        private Button _button;
        private GameObject _confirmPopup;
        [SerializeField] private Image classImage;
        [SerializeField] private TMP_Text classText;
    }
    
    public partial class CharClassCard : MonoBehaviour
    {
        private void Awake()
        {
            _button = GetComponent<Button>();
            _confirmPopup = TitleManager.Instance.confirmPopup;

            _button.onClick.AddListener(_ActivatePopup);
        }

        private void Start()
        {
            if (Data.ImageSourcePath != null)
            {
                classImage.sprite = Resources.Load<Sprite>(Data.ImageSourcePath);
            }
            classText.text = Data.ClassName;
        }
    }
    
    public partial class CharClassCard
    {
        private void _ActivatePopup()
        {
            if (Data.IsOpened)
            {
                TitleManager.Instance.selectedClassId = Data.Id;
                _confirmPopup.SetActive(true);
            }
            
        }
    }
}

