using System;
using Actor.Stats;

namespace Interface
{
    public interface IAffected
    {
        void Affected(Effect effect);
        void Released(Effect effect);
    }
}