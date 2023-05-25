using System;
using UnityEngine;

namespace Skill.Projectile
{
    [Serializable]
    public struct ProjectileData
    {
        public Sprite sprite;
        public float radius;
        public float speed;
        public string targetTag;
    }
}