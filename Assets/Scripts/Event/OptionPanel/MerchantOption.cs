using Items;
using TMPro;
using UnityEngine;

public class MerchantOption : MonoBehaviour
{
    private GameManager _gameManager;

    private Item _item;
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            ChangeTextToItem();
        }
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void Buy()
    {
        // if (Item의 가격 < _gameManager.Gold)
        // {
        //     _gameManager.Gold = Item의 가격;
        //     Player.inventory.AddItem();
        // this.transform.parent.parent.GetComponent<MerchantUI>().CloseUI(); 
        // }
        // else
        // {
        //     //      Debug.Log("골드가 부족합니다.");
        // }
    }

    private void ChangeTextToItem()
    {
        TextMeshProUGUI tMP = this.GetComponentInChildren<TextMeshProUGUI>();

        tMP.text = _item.itemName;
    }
}
