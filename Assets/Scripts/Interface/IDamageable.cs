
using System;
using Actor.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interface
{
    [Serializable]
    public struct DamageData
    {
        public ElementalType elementalType;
        public int damage;
        public Vector3 kbForce;
    }
    
    public interface IDamageable
    {
        void GetHit(DamageData data);
        void DotDamaged(DamageData data, float duration);
    }
}


