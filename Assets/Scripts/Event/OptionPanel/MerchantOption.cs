using Items;
using Items.ScriptableObjectSource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantOption : MonoBehaviour
{
    private GameManager _gameManager;
    private Item _item;
    private Inventory _playerInventory;

    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            ChangeTextToEqupment();
        }
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerInventory = FindObjectOfType<Inventory>(); // 임시 Player가 정해지면 수정예정
        GetComponent<Button>().onClick.AddListener(Buy);
    }

    public void Buy()
    {
        if (_item.gold <= _gameManager.Gold)
        {
            _gameManager.Gold -= _item.gold;
            Equipment equipment = (Equipment)_item;
            _playerInventory.AddItem(equipment.id);
            this.transform.parent.parent.GetComponent<MerchantUI>().CloseUI(); 
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    private void ChangeTextToEqupment()
    {
        TextMeshProUGUI tMP = this.GetComponentInChildren<TextMeshProUGUI>();

        tMP.text = "[ " + _item.gold + "G ] " + _item.itemName;
    }
}
