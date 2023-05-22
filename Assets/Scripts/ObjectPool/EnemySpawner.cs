using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Pool;
using Actor.Enemy;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    // Values or methods that other can use
    public partial class EnemySpawner
    {
        public static EnemySpawner Instance => _instance;
        public Dictionary<int, GameObject> GetAllEnemies() => _activeEnemies;

        public Action<int> OnClearBattle;
    }
    
    // Values or methods that other cannot use
    public partial class EnemySpawner
    {
        private static EnemySpawner _instance;
        
        private IObjectPool<Enemy> _pool;
        private List<Transform> _spawnPos = new List<Transform>();
        private Dictionary<int, GameObject> _activeEnemies = new Dictionary<int, GameObject>();
        private WaitForSeconds _spawnWait;

        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnDuration = 10f;
        [Description("Maximun spawn count in the same time")]
        [SerializeField] private int poolMax = 20;
        [Description("Total spawn count in this stage")]
        [SerializeField] private int maxCount = 50;
        private int _currentCount;
        private int _killedCount;
    }
    
    // body of MonoBehaviour
    public partial class EnemySpawner : MonoBehaviour
    {
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            
            _spawnWait = new WaitForSeconds(spawnDuration);
            _pool = new ObjectPool<Enemy>(CreateEnemy, ActivateEnemy, DeActivateEnemy, OnDestroyEnemy, maxSize:poolMax);
            
            foreach (Transform child in transform)
            {
                _spawnPos.Add(child);
            }
        }

        void Start()
        {
            _currentCount = 0;
            _killedCount = 0;
            
            // TODO : Change method of spawn by stored data
            StartCoroutine(_SpawnEnemy());
        }
    }
    
    // body of others
    public partial class EnemySpawner
    {
        private IEnumerator _SpawnEnemy()
        {
            while (_currentCount < maxCount)
            {
                Vector3 pos = _spawnPos[Random.Range(0, _spawnPos.Count - 1)].position;
                Enemy enemy = _pool.Get();
                enemy.transform.position = pos;
                enemy.spawnId = _currentCount;
                _activeEnemies[enemy.spawnId] = enemy.gameObject;
                
                yield return _spawnWait;
                _currentCount++;
            }
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            enemy.SetManagedPool(_pool);
            return enemy;
        }

        private void ActivateEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }
        
        private void DeActivateEnemy(Enemy enemy)
        {
            _activeEnemies.Remove(enemy.spawnId);
            _killedCount++;
            if (_killedCount == maxCount)
                OnClearBattle.Invoke(_killedCount);
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}