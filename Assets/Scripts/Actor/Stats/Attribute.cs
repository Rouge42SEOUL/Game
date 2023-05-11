using System;
using UnityEngine.Serialization;

namespace Actor.Stats
{
    [Serializable]
    public class Attribute
    {
        public AttributeType type;
        [FormerlySerializedAs("baseValue")] public float value;

        public Attribute(AttributeType type, float value)
        {
            this.type = type;
            this.value = value;
        }
    }
}