
using System;
using UnityEngine;

namespace Interface
{
    [Serializable]
    public struct DamageData
    {
        public int Damage;
        public Vector3 KbForce;
    }
    
    public interface IDamageable
    {
        void GetHit(DamageData data);
        void DotDamaged(DamageData data, float duration);
    }
}


