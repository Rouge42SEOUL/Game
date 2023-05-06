
using UnityEngine;

namespace Interface
{
    public struct DamageData
    {
        public int Damage;
        public Vector3 KbForce;
    }
    
    public interface IDamageable
    {
        void GetHit(DamageData data);
    }
}


