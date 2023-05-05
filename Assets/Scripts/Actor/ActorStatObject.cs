
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
    public class ActorStatObject : ScriptableObject
    {
        public float health;
        public float atkPower;
        public float speed;
        public float defence;
        public ElementalType element;
    }
}


