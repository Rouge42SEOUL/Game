using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Player;
using ObjectPool;
using TMPro;
using UnityEngine;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] private GameObject _enemyCount;
    [SerializeField] private GameObject _elapsedTime;
    [SerializeField] private GameObject _earnedMoney;
    [SerializeField] private GameObject _earnedExp;
    
    private WaitForSeconds _wait = new WaitForSeconds(0.5f);
    private GameObject _panel;
    private Player _player;

    private int count;
    private int money;
    private int exp;

    private void Awake()
    {
        _enemyCount.SetActive(false);
        _elapsedTime.SetActive(false);
        _earnedMoney.SetActive(false);
        _earnedExp.SetActive(false);
        
        _panel = transform.GetChild(0).gameObject;
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
        count = killCount;
        money = killCount * 10;
        exp = killCount;
        // TODO: add money and exp to player
        _enemyCount.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count.ToString("N0");
        // TODO: get clear time in timer
        _elapsedTime.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00:00";
        // TODO: replace tmp values in calculation
        _earnedMoney.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = money.ToString("N0");
        _earnedExp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = exp.ToString("N0");
        StartCoroutine(DisplayUI());
    }

    IEnumerator DisplayUI()
    {
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
