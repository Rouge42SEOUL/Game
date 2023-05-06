using System.Collections.Generic;
using UnityEngine;


namespace Actor.Stats
{
    public class EffectMachine : MonoBehaviour
    {
        public List<Effect> effects = new List<Effect>();

        public void SufferEffect(Effect effect, Actor actor)
        {
            // 이미 해당 상태이상이 적용되어 있는 경우
            if (effects.Exists(e => e.type == effect.type))
            {
                if (effect.isPermanent)
                    return;
                else if (effect.isStackable)
                {
                    if (effect.overlappingCount < 2)
                    {
                        effects.Add(effect);
                        effect.overlappingCount++;
                    }
                    else
                    {
                        float changeValue;
                        switch (effect.type)
                        {
                            case EffectType.Burns:
                                changeValue = effect.effectivePoint;
                                actor.stat.defence -= changeValue;
                                break;
                            case EffectType.Frostbite:
                                actor.stat.moveSpeed = 0;
                                break;
                            case EffectType.Poison:
                                changeValue = actor.stat.health * effect.effectivePoint;
                                actor.stat.health -= changeValue;
                                break;
                        }
                    }
                }
                else
                {
                    var existingEffect = effects.Find(e => e.type == effect.type);
                    existingEffect.duration = effect.duration;
                }

            }
            // 새로운 상태이상을 적용하는 경우
            else
            {
                effects.Add(effect);
            }

            switch (effect.type)
            {
                case EffectType.Bleeding:
                {
                    var changeValue = actor.stat.entireHP * effect.effectivePoint;
                    actor.stat.currentHp -= changeValue;
                    break;
                }
                case EffectType.Blind:
                {
                    actor.stat.accuracyRate = 0;
                    break;
                }
                case EffectType.Burns:
                {
                    var changeValue = effect.effectivePoint;
                    actor.stat.currentHp -= changeValue;
                    break;
                }
                case EffectType.Confuse:
                {
                    /*
                     * if(player)
                     *  조작 반전
                     * if(enemy)
                     *  공격 타겟 변경(가까운 다른 enemy)
                     */
                    break;
                }
                case EffectType.Fracture:
                {
                    var changeValue = effect.effectivePoint;
                    actor.stat.moveSpeed -= changeValue;
                    break;
                }
                case EffectType.Frostbite:
                {
                    var changeValue = effect.effectivePoint;
                    actor.stat.moveSpeed -= changeValue;
                    break;
                }
                case EffectType.Paralysis:
                {
                    var changeValue = effect.effectivePoint;
                    actor.stat.atkSpeed -= changeValue;
                    break;
                }
                case EffectType.Poison:
                {
                    var changeValue = actor.stat.entireHP * effect.effectivePoint;
                    actor.stat.currentHp -= changeValue;
                    break;
                }

            }
        }

    }
}
    