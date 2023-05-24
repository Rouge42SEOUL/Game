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
        
        [SerializeField] private int firstGold;
        [SerializeField] private string jsonFileName = "Json/GameManager.json";

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
            if (StageManager.Instance)
                return false;
            _data.Map = StageManager.Instance.MapNum;
            _data.SaveInfo(StageManager.Instance.Nodes, MapDataManager.Instance.CurrentNode);
            JsonConverter.Save(_data, Application.dataPath + jsonFileName);
            return true;
        }

        public bool LoadData()
        {
            return JsonConverter.Load(out _data, Application.dataPath + jsonFileName);
        }
        
    }
}