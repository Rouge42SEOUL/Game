using System;
using UnityEngine;

namespace Skill.Projectile
{
    [Serializable]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileData _projectileData;
        private ProjectileLauncher _launcher;
        private bool _isCollided = false;
        
        public float Speed => _projectileData.speed;

        public void SetLauncher(ProjectileLauncher launcher)
        {
            _launcher = launcher;
        }

        private void OnEnable()
        {
            _isCollided = false;
            GetComponent<SpriteRenderer>().sprite = _projectileData.sprite;
            GetComponent<CircleCollider2D>().radius = _projectileData.radius;
            float scale = _projectileData.radius * 2;
            transform.localScale = new Vector3(scale, scale, scale);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isCollided) 
                return;
            if (other.CompareTag(_projectileData.targetTag) || other.CompareTag("Obstacle"))
            {
                _isCollided = true;
                _launcher.OnAttackTrigger?.Invoke(other.gameObject);
                _launcher.Release(this);
            }
        }
    }
}