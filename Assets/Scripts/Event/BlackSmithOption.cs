using Items;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BlackSmithOption : MonoBehaviour
{
    private GameManager _gameManager;

    public Equipment Equipment
    {
        get => Equipment;
        set
        {
            Equipment = value;
            ChangeTextToEqupment();
        }
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void Upgrade()
    {
        // if (Equipment의 가격보다 게임매니저의 Gold가 더 많으면)
        // {
        //     Equipment.level++;
        //     _gameManager.Gold -= Equipment의 가격
        //     this.transform.parent.parent.gameObject.SetActive(false); 
        // }
        // else
        // {
        //      Debug.Log("골드가 부족합니다.");
        // }
    }

    private void ChangeTextToEqupment()
    {
        TextMeshProUGUI tMP = this.GetComponentInChildren<TextMeshProUGUI>();

        tMP.text = "Equpment";
        // tMP.text = Equipment.itemName;
    }
    
}
