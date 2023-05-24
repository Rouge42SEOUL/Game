
using UnityEngine;

namespace Managers.MainScene
{
    public partial class PassiveSelectView
    {
        
    }
    
    public partial class PassiveSelectView
    {
        private GameObject _passiveCard;
    }
    
    public partial class PassiveSelectView : MonoBehaviour
    {
        private void Awake()
        {
            _passiveCard = Resources.Load<GameObject>("Prefab/UI Prefab/Passive Select Button");

            int i = Random.Range(0, TitleManager.Instance.PassiveList.Count);
            int j = (i + Random.Range(1, TitleManager.Instance.PassiveList.Count)) %
                    TitleManager.Instance.PassiveList.Count;
            GameObject temp = Instantiate(_passiveCard, transform);
            temp.GetComponent<PassiveClassCard>().Data =  TitleManager.Instance.PassiveList[i];
            GameObject temp2 = Instantiate(_passiveCard, transform);
            temp2.GetComponent<PassiveClassCard>().Data = TitleManager.Instance.PassiveList[j];
        }
    }
    
    public partial class PassiveSelectView
    {
        
    }
}
