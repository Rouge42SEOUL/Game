using System;
using Actor.Stats;
using Interface;

namespace Actor.Skill
{
    [Serializable]
    public class Skill
    {
        public int id;
        public string name;
        public string description;
        public DamageData data;
        public Skill()
        {
            id = -1;
            name = "";
            description = "";
        }

        public void EffectDetermination(Actor<ActorStatObject> target, DamageData damageData)
        {
            switch (damageData.elementalType)
            {
                case ElementalType.Fire:
                {
                    if (target.stat.elementalType == ElementalType.Wind)
                        damageData.damage *= 2;
                    var effect = new Effect(EffectType.Burns, 10, 5, false);
                    AffectedConfirm(target,effect);
                    break;
                }
                case ElementalType.Ice :
                {
                    if (target.stat.elementalType == ElementalType.Fire)
                        damageData.damage *= 2;
                    var effect = new Effect(EffectType.Frostbite, 10, 5, false);
                    AffectedConfirm(target,effect);
                    break;
                }
                case ElementalType.Ground :
                {
                    if (target.stat.elementalType == ElementalType.Ice)
                        damageData.damage *= 2;
                    var effect = new Effect(EffectType.Fracture, 5, false);
                    AffectedConfirm(target,effect);
                    break;
                }
                case ElementalType.Wind :
                {
                    if (target.stat.elementalType == ElementalType.Ground)
                        damageData.damage *= 2;
                    var effect = new Effect(EffectType.Bleeding, 5, false);
                    AffectedConfirm(target,effect);
                    break;
                }
                case ElementalType.Holy :
                {
                    if (target.stat.elementalType == ElementalType.Dark)
                        damageData.damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5, false);
                    AffectedConfirm(target,effect);
                    break;
                }
                case ElementalType.Dark :
                {
                    if (target.stat.elementalType == ElementalType.Holy)
                        damageData.damage *= 2;
                    var effect = new Effect(EffectType.Blind, 10, 5, false);
                    AffectedConfirm(target,effect);
                    break;
                }
            }
        }
        private static void AffectedConfirm(Actor<ActorStatObject> target, Effect effect)
        {
            var rand = new Unity.Mathematics.Random();
            var randDouble = rand.NextDouble();
            if (effect.type is EffectType.Bleeding or EffectType.Fracture)
            {
                if(randDouble < 0.01f)
                    Affected(target, effect);
            }
            else
            {
                if(randDouble < 0.1f)
                    Affected(target, effect);
            }
        }
        
        private static void Affected(Actor<ActorStatObject> target, Effect effect)
        {
            target.skillEffectValues[effect.effectTo[0]] = effect.effectValue;
            target.stat.effects.Add(effect);
        }
    }
}