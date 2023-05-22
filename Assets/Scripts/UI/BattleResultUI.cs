using System.Collections;
using Actor.Player;
using ObjectPool;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleResultUI : MonoBehaviour
    {
        [SerializeField] GameObject[] _stars = new GameObject[3];
        [SerializeField] private GameObject _enemyCount;
        [SerializeField] private GameObject _elapsedTime;
        [SerializeField] private GameObject _earnedMoney;
        [SerializeField] private GameObject _earnedExp;
    
        private WaitForSeconds _wait = new WaitForSeconds(0.5f);
        private GameObject _panel;
        private Player _player;

        private int _count;
        private int _money;
        private int _exp;
        private bool[] _isClear = new bool[3];

        private void Awake()
        {
            _enemyCount.SetActive(false);
            _elapsedTime.SetActive(false);
            _earnedMoney.SetActive(false);
            _earnedExp.SetActive(false);
        
            _panel = transform.GetChild(0).gameObject;
            var stageGoal = _panel.transform.GetChild(1);
            for (int i = 0; i < 3; i++)
            {
                _stars[i] = stageGoal.GetChild(i).gameObject;
                _stars[i].SetActive(false);
            }
            _panel.SetActive(false);
            _player = GameObject.Find("Player").GetComponent<Player>();
        }

        private void Start()
        {
            EnemySpawner.Instance.OnClearBattle += OnClearBattle;
        }

        private void OnDestroy()
        {
            EnemySpawner.Instance.OnClearBattle -= OnClearBattle;
        }

        private void OnClearBattle(int killCount)
        {
            _count = killCount;
            _money = killCount * 10;
            _exp = killCount;
            // TODO: add money and exp to player
            _enemyCount.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _count.ToString("N0");
            // TODO: get clear time in timer
            _elapsedTime.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00:00";
            // TODO: replace tmp values in calculation
            _earnedMoney.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _money.ToString("N0");
            _earnedExp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _exp.ToString("N0");
            _isClear[0] = true;
            _isClear[1] = _player.PercentHealPoint >= 0.8f;
            _isClear[2] = _player.PercentHealPoint >= 0.6f;
            StartCoroutine(DisplayUI());
        }

        IEnumerator DisplayUI()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_isClear[i])
                {
                    _stars[i].SetActive(true);
                    yield return _wait;
                }
            }
            _panel.SetActive(true);
            yield return _wait;
            _enemyCount.SetActive(true);
            yield return _wait;
            _elapsedTime.SetActive(true);
            yield return _wait;
            _earnedMoney.SetActive(true);
            yield return _wait;
            _earnedExp.SetActive(true);
        }
    }
}
