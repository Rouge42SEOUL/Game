using System;
using Actor.Stats;

namespace Interface
{
    public interface IAffected
    {
        void Affected(Effect effect, Func<float, float> getValueToAdd);
        void Realeased(Effect effect);
    }
}