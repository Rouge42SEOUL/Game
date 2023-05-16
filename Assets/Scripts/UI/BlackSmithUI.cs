using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithUI : MonoBehaviour
{
    private GameObject[] _options;
    private Equipment[] _items;

    private void Start()
    {
        Transform childTransform = transform.Find("Options");
        Button[] childrenTransforms = childTransform.GetComponentsInChildren<Button>();
        _options = new GameObject[childrenTransforms.Length];
        _items = new Equipment[childrenTransforms.Length];
        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            _options[i] = childrenTransforms[i].gameObject;
        }
    }

    public void OptionRandomSetting()
    {
        Equipment[] items = new Equipment[] { }; // 장비 전체를 가져오는 함수를 사용할 예정
        List<Equipment> availableItems = new List<Equipment>();
        // 강화가능한 아이템들의 리스트를 availableItems에 등록
        for (int i = 0; i < items.Length; i++)
        {
            //if (items의 강화 수치가 최대 강화 수치인지, 강화 가능한 장비 아이템인지 체크)
                availableItems.Add(items[i]);
        }
        // 랜덤으로 리스트에서 뽑아서 _items에 등록
        for (int i = 0; i < _options.Length; i++)
        {
            _items[i] = availableItems[Random.Range(0, items.Length - 1)];
            // _items를 이용해서 UI에 표시
            TextMeshProUGUI tmp = _options[i].GetComponentInChildren<TextMeshProUGUI>();
            // tmp.text = _items[i].itemName + "(+" /* + _items[i].level  */  + ")" + ":" _items[i].damage ;
        }
    }

    public void DisplayUI()
    {
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
