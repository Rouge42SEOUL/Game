using System;
using UnityEngine;

namespace Actor.Skill
{
    [Serializable]
    public class Projectile : MonoBehaviour
    {
        private ProjectileLauncher _launcher;
        private ProjectileData _projectileData;
        private bool _isCollided = false;
        public float Speed => _projectileData.speed;
        
        public void SetData(ProjectileData projectile)
        {
            _projectileData.sprite = projectile.sprite;
            _projectileData.radius = projectile.radius;
            _projectileData.speed = projectile.speed;
            
            GetComponent<SpriteRenderer>().sprite = _projectileData.sprite;
            GetComponent<CircleCollider2D>().radius = _projectileData.radius;
            float scale = _projectileData.radius * 2;
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public void SetLauncher(ProjectileLauncher launcher)
        {
            _launcher = launcher;
        }

        private void OnEnable()
        {
            _isCollided = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isCollided) 
                return;
            if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
            {
                _isCollided = true;
                _launcher.OnAttackTrigger?.Invoke(other.gameObject);
                _launcher.Release(this);
            }
        }
    }
}