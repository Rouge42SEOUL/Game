using System;
using System.Collections;
using System.Collections.Generic;
using Managers.DataManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance ? _instance : null;
    private static GameManager _instance;

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
    }

    public void InitGame()
    {
        DataManager.Instance.DeleteData();
        DataManager.Instance.InitData();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }

}