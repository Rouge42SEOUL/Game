using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.DataManager
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance => _instance ? _instance : null;
        private static DataManager _instance;
        
        private DataContainer _data;
        private DataRunningEvent _runningData;
        
        [SerializeField] private int firstGold;
        [SerializeField] private string jsonFileName = "/Json/GameManager.json";
        private string _runningEventFile = "/Json/runningEvent.json";

        public Action<int> OnGoldUpdate;

        public int Gold
        {
            get => _data.Gold;
            set
            {
                _data.Gold = value;
                OnGoldUpdate?.Invoke(value);
            }
        }

        public int Map => _data.Map;
        public Dictionary<int, EventType> Events => _data.Events;
        public int CurrentNode => _data.PlayerCurrentNode;

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

        private void Start()
        {
            OnGoldUpdate?.Invoke(_data.Gold);
        }

        public void InitData()
        {
            Gold = firstGold;
        }

        public void InitEventKeys(ref int[] keys)
        {
            _data.Events.Keys.CopyTo(keys, 0);
        }

        public bool SaveData()
        {
            if (StageManager.Instance == null)
                return false;
            _data.Map = StageManager.Instance.MapNum;
            _data.SaveInfo(StageManager.Instance.Nodes, MapDataManager.Instance.CurrentNode);
            JsonConverter.Save(_data, Application.dataPath + jsonFileName);
            return true;
        }

        public bool GetRunningEvent()
        {
            JsonConverter.Load(out _runningData, Application.dataPath + _runningEventFile);
            return _runningData.IsEventRunning;
        }
        public void SetRunningEvent(bool b)
        {
            _runningData.IsEventRunning = b;
            JsonConverter.Save(_runningData, Application.dataPath + _runningEventFile);
        }

        public bool LoadData()
        {
            return JsonConverter.Load(out _data, Application.dataPath + jsonFileName);
        }

        public void DeleteData()
        {
            JsonConverter.DeleteJson(Application.dataPath + jsonFileName);
            JsonConverter.DeleteJson(Application.dataPath + _runningEventFile);
        }
    }
}