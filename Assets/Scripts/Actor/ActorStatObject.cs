
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor
{
    public enum ElementalType
    {
        Default,
        Fire,
        Ice,
        Wind,
        Ground,
        Light,
        Dark,
        Buff,
        DeBuff
    }

    [CreateAssetMenu(fileName = "Actor Data", menuName = "Scriptable Object/Actor Data")]
    public class ActorStat : ScriptableObject
    {
        public float health;
        public float atkPower;
        public float speed;
        public float defence;
        public ElementalType element;
    }

    public class ActorStatObject : ScriptableObject
    {
        public ActorStat baseStat;
        public float entireHp;
        public float currentHp;
        public float totalAtk;
        public float totalDfc;
        public float moveSpeed;
        public float atkSpeed;
        public float accuracyRate;
        public float avoidanceRate;
        
        public ActorStatObject(ActorStat baseStat)
        {
            this.baseStat = baseStat;
            this.entireHp = baseStat.health * 125.0f;
            this.currentHp = this.entireHp;
            this.totalAtk = baseStat.atkPower;
            this.totalDfc = baseStat.defence;
            this.moveSpeed = baseStat.speed * 1.025f;
            this.atkSpeed = baseStat.speed * 1.015f;
            this.accuracyRate = baseStat.speed * 0.01f + 0.5f;
            this.avoidanceRate = baseStat.speed * 0.0075f;
        }
    }
    
    
}


