using System.Collections.Generic;
using Items.ScriptableObjectSource;
using UnityEngine;
using UnityEngine.UI;

public class MerchantUI : EventUI
{ 
    [SerializeField] private EquipmentDatabase productList;
    private GameObject[] _options;

    protected override void Start()
    {
        base.Start();
        Transform childTransform = transform.Find("Options");
        Button[] childrenTransforms = childTransform.GetComponentsInChildren<Button>();
        _options = new GameObject[childrenTransforms.Length];
        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            _options[i] = childrenTransforms[i].gameObject;
        }
    }

    public void OptionRandomSetting()
    {
        List<Item> items = new List<Item>(productList.items);
        for (int i = 0; i < _options.Length; i++)
        {
            if (i < items.Count)
            {
                int randomValue = Random.Range(0, items.Count);
                _options[i].SetActive(true);
                _options[i].GetComponent<MerchantOption>().Item = 
                    items[randomValue];
                items.RemoveAt(randomValue);
            }
            else
            {
                _options[i].SetActive(false);
            }
        }
        // 아이템을 랜덤으로 골라서 패널 set에 넣어준다.
        // 패널에서 set한 아이템의 정보를 텍스트에 띄운다.
        // 패널의 OnClick 함수는 아이템을 살 수 있게 한다.
        // buy() 함수에서 게임매니저의 Gold랑 비교한다.
        // 돈이 되면 아이템을 플레이어가 얻고, 이벤트 창은 닫는다.
        // 돈이 안된다면 아무 일도 일어나지 않는다.
    }
}
