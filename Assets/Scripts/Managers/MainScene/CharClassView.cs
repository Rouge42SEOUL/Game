
using UnityEngine;

namespace Managers.MainScene
{
    public partial class CharClassView
    {
        
    }
    
    public partial class CharClassView
    {
        private GameObject _classCard;
    }
    
    public partial class CharClassView : MonoBehaviour
    {
        private void Awake()
        {
            _classCard = Resources.Load<GameObject>("Prefab/UI Prefab/Character Select Button");
            
            foreach (CharClassData data in TitleManager.Instance.ClassList)
            {
                GameObject temp = Instantiate(_classCard, transform);
                temp.GetComponent<CharClassCard>().Data = data;
            }
        }
    }
    
    public partial class CharClassView
    {
        
    }
}