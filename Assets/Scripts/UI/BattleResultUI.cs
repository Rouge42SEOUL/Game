using System.Collections;
using Actor.Player;
using Actor.Stats;
using Managers.DataManager;
using ObjectPool;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BattleResultUI : MonoBehaviour
    {
        private GameObject[] _stars = new GameObject[3];
        [SerializeField] private GameObject _enemyCount;
        [SerializeField] private GameObject _earnedMoney;
        [SerializeField] private GameObject _elapsedTime;
        [SerializeField] private GameObject _playerStat;
    
        private WaitForSeconds _wait = new WaitForSeconds(0.5f);
        private GameObject _panel;
        private Player _player;

        private int _count;
        private int _money;
        private bool[] _isClear = new bool[3];

        private void Awake()
        {
            _enemyCount.SetActive(false);
            _elapsedTime.SetActive(false);
            _earnedMoney.SetActive(false);
            _playerStat.SetActive(false);
        
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
            DataManager.Instance.Gold += _money;
            
            _enemyCount.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _count.ToString("N0");
            _earnedMoney.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _money.ToString("N0");
            _elapsedTime.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                BattleDataManager.Instance.Minutes.ToString("D2") + ":" + BattleDataManager.Instance.Seconds.ToString("f0");

            string con = DataManager.Instance.GetBaseStat(AttributeType.Health).ToString("N0");
            string agi = DataManager.Instance.GetBaseStat(AttributeType.Speed).ToString("N0");
            string atk = DataManager.Instance.GetBaseStat(AttributeType.Attack).ToString("N0");
            string def = DataManager.Instance.GetBaseStat(AttributeType.Defense).ToString("N0");
            DataManager.Instance.LevelUP();
            con += " -> " + DataManager.Instance.GetBaseStat(AttributeType.Health).ToString("N0");
            agi += " -> " + DataManager.Instance.GetBaseStat(AttributeType.Speed).ToString("N0");
            atk += " -> " + DataManager.Instance.GetBaseStat(AttributeType.Attack).ToString("N0");
            def += " -> " + DataManager.Instance.GetBaseStat(AttributeType.Defense).ToString("N0");
            
            _playerStat.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = con;
            _playerStat.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = agi;
            _playerStat.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = atk;
            _playerStat.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = def;
            
            _isClear[0] = true;
            _isClear[1] = _player.PercentHealPoint >= 0.8f;
            _isClear[2] = _player.PercentHealPoint >= 0.6f;
            StartCoroutine(DisplayUI());
        }

        public void OnClickContinue()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
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
            _earnedMoney.SetActive(true);
            yield return _wait;
            _elapsedTime.SetActive(true);
            yield return _wait;
            _playerStat.SetActive(true);
        }
    }
}
