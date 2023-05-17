using System.Collections.Generic;

namespace Elemental
{
    public static class ElementalBalancer
    {
        private struct EBalance
        {
            public readonly ElementalType WeakTo;
            public readonly ElementalType StrongTo;

            public EBalance(ElementalType w, ElementalType s)
            {
                WeakTo = w;
                StrongTo = s;
            }
        }

        private static readonly Dictionary<ElementalType, EBalance> Info = new Dictionary<ElementalType, EBalance>()
        {
            { ElementalType.Normal, new EBalance(ElementalType.None, ElementalType.None) },
            { ElementalType.Fire, new EBalance(ElementalType.Ice, ElementalType.Wind) },
            { ElementalType.Ice, new EBalance(ElementalType.Ground, ElementalType.Fire) },
            { ElementalType.Wind, new EBalance(ElementalType.Fire, ElementalType.Ground) },
            { ElementalType.Ground, new EBalance(ElementalType.Wind, ElementalType.Ice) },
            { ElementalType.Holy, new EBalance(ElementalType.Dark, ElementalType.Dark) },
            { ElementalType.Dark, new EBalance(ElementalType.Holy, ElementalType.Holy) },
        };

        public static float ApplyBalance(ElementalType type, ElementalType target, float damage)
        {
            if (Info[type].WeakTo == target)
                return 1f * damage;
            if (Info[type].StrongTo == target)
                return 2f * damage;
            return damage;
        }
    }
}