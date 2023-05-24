using System;
using Managers.DataManager;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    private TextMeshProUGUI _tMP;
	private int _gold;
    public int Gold
    {
        private get => _gold;
        set
        {
            _gold = value;
			_tMP.text = _gold.ToString();
        }
    }

    private void Awake()
    {
        _tMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        DataManager.Instance.OnGoldUpdate += OnGoldUpdated;
    }

    private void OnDisable()
    {
        DataManager.Instance.OnGoldUpdate -= OnGoldUpdated;
    }

    private void OnGoldUpdated(int gold)
    {
        Gold = gold;
    }
}
