using System.Collections.Generic;
using Items;
using Items.ScriptableObjectSource;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithUI : EventUI
{
    private GameObject[] _options;
    private Inventory _playerInventory;

    protected override void Start()
    {
        base.Start();
        Transform childTransform = transform.Find("Options");
        _playerInventory = FindObjectOfType<Inventory>(); // 임시 Player가 정해지면 수정예정
        Button[] childrenTransforms = childTransform.GetComponentsInChildren<Button>();
        _options = new GameObject[childrenTransforms.Length];
        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            _options[i] = childrenTransforms[i].gameObject;
        }
    }
    public void OptionRandomSetting()
    {
        List<Equipment> equipments = _playerInventory.RequireTotalEquipments();
        List<Equipment> availableItems = new List<Equipment>();
        // 강화가능한 아이템들의 리스트를 availableItems에 등록
        for (int i = 0; i < equipments.Count; i++)
        {
            //if (items의 강화 수치가 최대 강화 수치인지, 강화 가능한 장비 아이템인지 체크)
            availableItems.Add(equipments[i]);
        }
        // 랜덤으로 리스트에서 뽑아서 _equipments에 등록
        for (int i = 0; i < _options.Length; i++)
        {
            if (i < availableItems.Count)
            {
                int randomValue = Random.Range(0, availableItems.Count);
                _options[i].GetComponent<BlackSmithOption>().Equipment
                    = availableItems[randomValue];
                availableItems.RemoveAt(randomValue);
            }
            else
            {
                _options[i].SetActive(false);
            }
        }
    }
}
