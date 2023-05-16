using System;
using Interface;
using UnityEngine;
using UnityEngine.Pool;

namespace Actor.Skill
{
    public partial class ProjectileLauncher
    {
        private IObjectPool<Projectile> _pool;
        private GameObject _context;
        private readonly int _poolMax = 10;
        [SerializeField] private GameObject projectilePrefab;
        
        public Action<GameObject> OnAttackTrigger;

        public void SetContext(GameObject context)
        {
            _context = context;
        }

        public void Release(Projectile projectile) => _pool.Release(projectile);
    }

    public partial class ProjectileLauncher : MonoBehaviour
    {
        private void Start()
        {
            _pool = new ObjectPool<Projectile>(CreateProjectile, OnGetProjectile, OnRealeaseProjectile,
                OnDestroyProjectile, maxSize: _poolMax);
        }
    }
    
    public partial class ProjectileLauncher
    {
        public void Launch(ProjectileData data)
        {
            Projectile projectile = _pool.Get();
            projectile.SetLauncher(this);
            projectile.transform.position = transform.position;
            projectile.SetData(data);
            Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(_context.GetComponent<IActorContext>().Forward * projectile.Speed, ForceMode2D.Impulse);
        }

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
            return projectile;
        }

        private void OnGetProjectile(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }
        
        private void OnRealeaseProjectile(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}