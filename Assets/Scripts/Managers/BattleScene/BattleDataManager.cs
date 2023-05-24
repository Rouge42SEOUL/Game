
using Actor.Player;
using Actor.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDataManager : MonoBehaviour
{
    public static BattleDataManager Instance => _instance ? _instance : null;
    public int Minutes => (int)(_t / 60);
    public float Seconds => _t % 60;
    
    #region SerializedFiledData

    [SerializeField] private Slider healthSlider;
    
    [SerializeField] private TMP_Text atkText;
    [SerializeField] private TMP_Text agiText;
    [SerializeField] private TMP_Text defText;

    [SerializeField] private TMP_Text timerText;
    
    #endregion

    #region Private_Values
    
    private static BattleDataManager _instance;
    private Player _player;
    private float _t;

    #endregion

    #region Mono_Functions

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        _t = 0;
    }

    private void Update()
    {
        healthSlider.value = _player.PercentHealPoint;
        atkText.text = "ATK : " + _player.GetAttributeValue(AttributeType.Attack);
        agiText.text = "AGI : " + _player.GetAttributeValue(AttributeType.Speed);
        defText.text = "DEF : " + _player.GetAttributeValue(AttributeType.Defense);

        _t += Time.deltaTime;
        timerText.text = _t.ToString("F2");
    }

    #endregion
}
