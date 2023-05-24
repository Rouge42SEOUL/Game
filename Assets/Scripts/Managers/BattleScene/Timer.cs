
using Actor.Player;
using Actor.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance => _instance ? _instance : null;
    public int Minutes => (int)(_t / 60);
    public float Seconds => _t % 60;
    
    #region SerializedFiledData

    [SerializeField] private TMP_Text timerText;
    
    #endregion

    #region Private_Values
    
    private static Timer _instance;
    private float _t;

    #endregion

    #region Mono_Functions

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        _t = 0;
    }

    private void Update()
    {
        _t += Time.deltaTime;
        timerText.text = Minutes + ":" + Seconds.ToString("F2");
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
