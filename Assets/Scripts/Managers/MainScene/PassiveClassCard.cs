using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Managers.MainScene
{
    public partial class PassiveClassCard
    {
        public PassiveData Data;
    }
    
    public partial class PassiveClassCard
    {
        private Button _button;
        private GameObject _confirmSelect;
        [SerializeField] private Image passiveImage;
        [SerializeField] private TMP_Text passiveText;
    }
    
    public partial class PassiveClassCard : MonoBehaviour
    {
        private void Awake()
        {
            _button = GetComponent<Button>();
            _confirmSelect = TitleManager.Instance.confirmPopup;

            _button.onClick.AddListener(_ActivatePopup);
        }

        private void Start()
        {
            Sprite[] s = Resources.LoadAll<Sprite>("Illustration/PassiveIcon/Elemental");
            passiveImage.sprite = s[Data.Id];
            
            passiveText.text = Data.PassiveName;
        }
    }
    
    public partial class PassiveClassCard
    {
        private void _ActivatePopup()
        {
            TitleManager.Instance.selectedPassiveId = Data.Id;
            TitleManager.Instance.SelectPassive(Data.Id);
            _confirmSelect.SetActive(true);
        }
    }
}