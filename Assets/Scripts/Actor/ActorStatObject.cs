
using UnityEngine;

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
        Buf,
        Debuf
    }
    [CreateAssetMenu(fileName = "Actor Data", menuName = "Scriptable Object/Actor Data")]
    public class ActorStatObject : ScriptableObject
    {
        public int health;
        public int atkPower;
        public int speed;
        public ElementalType element;
    }
}


