
using Actor.Player;
using Actor.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDataManager : MonoBehaviour
{
    #region SerializedFiledData

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text atkText;
    [SerializeField] private TMP_Text agiText;
    [SerializeField] private TMP_Text defText;
    
    #endregion

    #region Private_Values

    private Player _player;

    #endregion

    #region Mono_Functions

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        healthSlider.value = _player.PercentHealPoint;
        atkText.text = "ATK : " + _player.GetAttributeValue(AttributeType.Attack);
        agiText.text = "AGI : " + _player.GetAttributeValue(AttributeType.Speed);
        defText.text = "DEF : " + _player.GetAttributeValue(AttributeType.Defense);
    }

    #endregion
}
