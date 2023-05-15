using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using Actor.Enemy;

namespace ObjectPool
{
    // Values or methods that other can use
    public partial class EnemySpawner
    {
        
    }
    
    // Values or methods that other cannot use
    public partial class EnemySpawner
    {
        private IObjectPool<Enemy> _pool;
        private List<Transform> _spawnPos = new List<Transform>();
        private WaitForSeconds _spawnWait;

        [SerializeField] private float spawnDuration = 10f;
        [SerializeField] private int maxCount = 20;
        [SerializeField] private GameObject enemyPrefab;
    }
    
    // body of MonoBehaviour
    public partial class EnemySpawner : MonoBehaviour
    {
        private void Awake()
        {
            _spawnWait = new WaitForSeconds(spawnDuration);
            _pool = new ObjectPool<Enemy>(CreateEnemy, ActivateEnemy, DeActivateEnemy, OnDestroyEnemy, maxSize:maxCount);
            
            foreach (Transform child in transform)
            {
                _spawnPos.Add(child);
            }
        }

        void Start()
        {
            // TODO : Change method of spawn by stored data
            StartCoroutine(_SpawnEnemy());
        }
    }
    
    // body of others
    public partial class EnemySpawner
    {
        private IEnumerator _SpawnEnemy()
        {
            while (true)
            {
                Vector3 pos = _spawnPos[Random.Range(0, _spawnPos.Count - 1)].position;
                Enemy enemy = _pool.Get();
                enemy.transform.position = pos;

                yield return _spawnWait;
            }
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
            enemy.SetManagedPool(_pool);
            return enemy;
        }

        private void ActivateEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.Init();
        }
        
        private void DeActivateEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}