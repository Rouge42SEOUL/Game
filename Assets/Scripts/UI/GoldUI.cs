using System;
using Managers.DataManager;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    private TextMeshProUGUI _tMP;

    private void Awake()
    {
        _tMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        DataManager.Instance.OnGoldUpdate += OnGoldUpdated;
        _tMP.text = DataManager.Instance.Gold.ToString();
    }

    private void OnDisable()
    {
        if (DataManager.Instance == null)
            return;
        DataManager.Instance.OnGoldUpdate -= OnGoldUpdated;
    }

    private void OnGoldUpdated(int gold)
    {
        _tMP.text = gold.ToString();
    }
}
