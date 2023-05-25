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
        private PlayData _playData = new ();
        [SerializeField] private PlayerStatObject _stat;

        private int _firstGold;
        private readonly string _mapDataPath = "/Json/MapData.json";
        private readonly string _playDataPath = "/Json/PlayData.json";
        //private readonly string _statDataPath = "/Json/StatData.json";

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
            _playData.Gold = _firstGold;
            _playData.CurrentNodeIdx = 0;
            _playData.IsEventRunning = false;
            
            _stat.InitStat();
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
            _mapData.SaveInfo();
            JsonConverter.Save(_mapData, Application.dataPath + _mapDataPath);
            JsonConverter.Save(_playData, Application.dataPath + _playDataPath);
            //JsonConverter.Save(_stat, Application.dataPath + _statDataPath);
            Debug.Log("Save Data");
            return true;
        }

        public void SavePlayData()
        {
            for (int i = 0; i < StageManager.Instance.Nodes.Length; i++)
            {
                if (StageManager.Instance.Nodes[i] == MapDataManager.Instance.CurrentNode)
                {
                    CurrentNode = i;
                }
            }
            JsonConverter.Save(_playData, Application.dataPath + _playDataPath);
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
            var isLoaded = JsonConverter.Load(out _mapData, Application.dataPath + _mapDataPath);
            isLoaded = isLoaded && JsonConverter.Load(out _playData, Application.dataPath + _playDataPath);
            //isLoaded = isLoaded && JsonConverter.Load(out _stat, Application.dataPath + _statDataPath);
            return isLoaded;
        }

        public void DeleteData()
        {
            JsonConverter.DeleteJson(Application.dataPath + _mapDataPath);
            JsonConverter.DeleteJson(Application.dataPath + _playDataPath);
            //JsonConverter.DeleteJson(Application.dataPath + _statDataPath);
        }

        public bool HasData() => File.Exists(Application.dataPath + _mapDataPath);
        public void LevelUP() => _stat.LevelUp();
        public float GetBaseStat(AttributeType type) => _stat.baseAttributes[type].value;
    }
}