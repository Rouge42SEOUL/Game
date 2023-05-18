
using System;
using UnityEngine;

namespace Interface
{
    [Serializable]
    public struct DamageData
    {
        public int Damage;
        public Vector3 KbForce;

        public DamageData(int d)
        {
            Damage = d;
            KbForce = Vector3.zero;
        }
    }
    
    public interface IDamageable
    {
        void Damaged(DamageData data);
        void DotDamaged(DamageData data, float duration);
    }
}


