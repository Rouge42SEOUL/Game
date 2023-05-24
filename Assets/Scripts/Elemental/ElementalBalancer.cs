using System;
using System.Collections.Generic;
using Actor.Stats;
using Interface;

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
            { ElementalType.Buff, new EBalance(ElementalType.Buff, ElementalType.DeBuff) },
            { ElementalType.DeBuff, new EBalance(ElementalType.DeBuff, ElementalType.Buff) },
        };

        public static float ApplyBalance(ElementalType type, ElementalType target, float damage)
        {
            if (Info[type].WeakTo == target)
                return 1f * damage;
            if (Info[type].StrongTo == target)
                return 2f * damage;
            return damage;
        }

        public static void ApplyElementalEffect(ElementalType type, ref Effect effect)
        {
            switch (type)
            {
                case ElementalType.Fire:
                {
                    if (GetProbability(EffectType.Burns))
                    {
                        effect = new Effect(EffectType.Burns, 10, 5);
                    }

                    break;
                }
                case ElementalType.Ice:
                {
                    if (GetProbability(EffectType.Frostbite))
                    {
                        effect = new Effect(EffectType.Frostbite, 10, 5);
                    }

                    break;
                }
                case ElementalType.Ground:
                {
                    if (GetProbability(EffectType.Fracture))
                    {
                        effect = new Effect(EffectType.Fracture, 5);
                    }

                    break;
                }
                case ElementalType.Wind:
                {
                    if (GetProbability(EffectType.Bleeding))
                    {
                        effect = new Effect(EffectType.Bleeding, 5);
                    }

                    break;
                }
                case ElementalType.Holy:
                {
                    if (GetProbability(EffectType.Blind))
                    {
                        effect = new Effect(EffectType.Blind, 10, 5);
                    }

                    break;
                }
                case ElementalType.Dark:
                {
                    if (GetProbability(EffectType.Blind))
                    {
                        effect = new Effect(EffectType.Blind, 10, 5);
                    }

                    break;
                }
                case ElementalType.Normal:
                case ElementalType.None:
                case ElementalType.Buff:
                case ElementalType.DeBuff:
                default:
                    break;
            }
        }

        private static bool GetProbability(EffectType type)
        {
            var rand = new Unity.Mathematics.Random();
            var randDouble = rand.NextDouble();
            if (type is EffectType.Bleeding or EffectType.Fracture)
            {
                if (randDouble < 0.01f)
                    return true;
            }
            else
            {
                if (randDouble < 0.1f)
                    return true;
            }
            return false;
        }
    }
}