
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
        public int health;
        public int atkPower;
        public int speed;
        public int defence;
        public ElementalType element;
    }
}


