using System;
using System.Collections.Generic;
using Actor.Stats;
using Managers.SaveData;
using UnityEngine;
using UnityEngine.Windows;

namespace Managers.DataManager
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance => _instance ? _instance : null;
        private static DataManager _instance;

        private MapData _mapData = new ();
        private PlayData _playData;
        [SerializeField] private PlayerStatObject _stat;

        private int _firstGold = 10000;
        private readonly string _mapDataPath = "/Json/MapData.json";
        private readonly string _playDataPath = "/Json/PlayData.json";

        public Action<int> OnGoldUpdate;

        public int Gold
        {
            get
            {
                if (_mapData == null)
                    return -1;
                return _playData.Gold;
            }
            set
            {
                if (_mapData == null)
                    return;
                _playData.Gold = value;
                OnGoldUpdate?.Invoke(value);
            }
        }

        public int MapIndex => _mapData.MapIndex;
        public Dictionary<int, EventType> Events => _mapData.Events;

        public int CurrentNode
        {
            get => _playData.CurrentNodeIdx;
            set => _playData.CurrentNodeIdx = value;
        }

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

        public void InitData()
        {
            _playData = new PlayData
            {
                Gold = _firstGold,
                CurrentNodeIdx = 0,
                IsEventRunning = false
            };
        }

        public void InitEventKeys(ref int[] keys)
        {
            _mapData.Events.Keys.CopyTo(keys, 0);
        }

        public bool SaveData()
        {
            if (StageManager.Instance == null)
                return false;
            _mapData.MapIndex = StageManager.Instance.MapNum;
            _mapData.SaveInfo(StageManager.Instance.Nodes, MapDataManager.Instance.CurrentNode);
            JsonConverter.Save(_mapData, Application.dataPath + _mapDataPath);
            JsonConverter.Save(_playData, Application.dataPath + _playDataPath);
            Debug.Log("Save Data");
            return true;
        }

        public bool GetRunningEvent()
        {
            JsonConverter.Load(out _playData, Application.dataPath + _playDataPath);
            return _playData.IsEventRunning;
        }
        
        public void SetRunningEvent(bool b)
        {
            _playData.IsEventRunning = b;
            JsonConverter.Save(_playDataPath, Application.dataPath + _playDataPath);
        }

        public bool LoadData()
        {
            JsonConverter.Load(out _playData, Application.dataPath + _playDataPath);
            return JsonConverter.Load(out _mapData, Application.dataPath + _mapDataPath);
        }

        public void DeleteData()
        {
            JsonConverter.DeleteJson(Application.dataPath + _mapDataPath);
            JsonConverter.DeleteJson(Application.dataPath + _playDataPath);
            JsonConverter.DeleteJson(Application.dataPath + "/Json/item.json");
        }

        public bool HasData() => File.Exists(Application.dataPath + _mapDataPath);
        public void LevelUP() => _stat.LevelUp();
        public float GetBaseStat(AttributeType type) => _stat.baseAttributes[type].value;
    }
}