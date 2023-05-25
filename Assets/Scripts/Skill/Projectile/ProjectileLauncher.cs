using System;
using Interface;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Skill.Projectile
{
    public partial class ProjectileLauncher
    {
        private IObjectPool<Projectile> _pool;
        private GameObject _context;
        
        private readonly int _poolMax = 10;
        private GameObject _projectilePrefab;
        
        public Action<GameObject> OnAttackTrigger;

        public void SetContext(GameObject context)
        {
            _context = context;
        }
        
        public void SetProjectile(GameObject projectile)
        {
            _projectilePrefab = projectile;
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
        public void Launch()
        {
            Projectile projectile = _pool.Get();
            projectile.SetLauncher(this);
            projectile.transform.position = transform.position;
            Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(_context.GetComponent<IActorContext>().Forward * projectile.Speed, ForceMode2D.Impulse);
        }

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(_projectilePrefab, transform).GetComponent<Projectile>();
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