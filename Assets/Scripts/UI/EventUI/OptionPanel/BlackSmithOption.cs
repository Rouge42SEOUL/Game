using Items.ScriptableObjectSource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class BlackSmithOption : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private Equipment _equipment;

    public Equipment Equipment
    {
        get => _equipment;
        set
        {
            _equipment = value;
            ChangeTextToEqupment();
        }
    }

    private void Start()
    {
        _gameSceneManager = GameSceneManager.Instance;
        GetComponent<Button>().onClick.AddListener(Upgrade);
    }

    public void Upgrade()
    {
        if (_equipment.gold + _equipment.reinforcement * 10 <= _gameSceneManager.Gold)
        {
            
            _gameSceneManager.Gold -= _equipment.gold + _equipment.reinforcement * 10;
            Equipment.reinforcement++;
            this.transform.parent.parent.gameObject.GetComponent<BlackSmithUI>().CloseUI(); 
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    private void ChangeTextToEqupment()
    {
        TextMeshProUGUI tMP = this.GetComponentInChildren<TextMeshProUGUI>();

        tMP.text = "[ " + (_equipment.gold + _equipment.reinforcement * 10) + "G ] " + _equipment.itemName + "(+" + _equipment.reinforcement + ")";
    }
    
}
