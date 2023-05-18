using System;

namespace Actor.Stats
{
    [Serializable]
    public class Attribute
    {
        public AttributeType type;
        public float value;

        public Attribute(AttributeType type, float value)
        {
            this.type = type;
            this.value = value;
        }
    }
}