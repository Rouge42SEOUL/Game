using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithUI : MonoBehaviour
{
    private GameObject[] _options;
    private GameObject _eventUIButton;

    private void Start()
    {
        Transform childTransform = transform.Find("Options");
        _eventUIButton = GameObject.Find("EventUIButton");
        Button[] childrenTransforms = childTransform.GetComponentsInChildren<Button>();
        _options = new GameObject[childrenTransforms.Length];
        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            _options[i] = childrenTransforms[i].gameObject;
        }
    }

    public void OptionRandomSetting()
    {
        Equipment[] equipments = new Equipment[] { }; // 장비 전체를 가져오는 함수를 사용할 예정
        List<Equipment> availableItems = new List<Equipment>();
        List<int> numbers = new List<int>();
        // 강화가능한 아이템들의 리스트를 availableItems에 등록
        for (int i = 0; i < equipments.Length; i++)
        {
            //if (items의 강화 수치가 최대 강화 수치인지, 강화 가능한 장비 아이템인지 체크)
                availableItems.Add(equipments[i]);
                numbers.Add(i);
        }
        // 랜덤으로 리스트에서 뽑아서 _equipments에 등록
        for (int i = 0; i < _options.Length; i++)
        {
            if (i < availableItems.Count)
            {
                int randomValue = Random.Range(0, numbers.Count);
                _options[i].GetComponent<BlackSmithOption>().Equipment
                    = availableItems[numbers[randomValue]];
                numbers.RemoveAt(randomValue);
            }
            else
            {
                _options[i].SetActive(false);
            }
        }
    }

    public void DisplayUI()
    {
        gameObject.SetActive(true);
        _eventUIButton.gameObject.SetActive(false);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
        _eventUIButton.gameObject.SetActive(true);
    }
}
