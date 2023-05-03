using System;

namespace Actor.Stats
{
    [Serializable]
    public class Attribute
    {
        public AttributeType type;
        public int baseValue;
        public int currentValue;

        public Attribute(AttributeType type, int value)
        {
            this.type = type;
            this.baseValue = value;
            this.currentValue = value;
        }
    }
}