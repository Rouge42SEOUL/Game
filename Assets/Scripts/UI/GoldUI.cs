using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private int gold;
    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            SetGold();
        }
    }
    private TextMeshProUGUI _TMP;
    
    private void Start()
    {
        _TMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        SetGold();
    }

    public void SetGold()
    {
        _TMP.text = gold.ToString();
    }
}
