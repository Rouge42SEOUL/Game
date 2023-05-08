using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Actor.Stats
{
    public class EffectMachine : MonoBehaviour
    {
        public List<Effect> effects = new List<Effect>();

        public void SufferEffect(Effect effect, Actor actor, float deltaTime)
        {
            // 이미 해당 상태이상이 적용되어 있는 경우
            if (effects.Exists(e => e.type == effect.type))
            {
                if (effect.isPermanent)
                    return;
                else if (effect.isStackable)
                {
                    if (effect.overlappingCount == 1)
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
                                actor.stat.totalDfc -= changeValue;
                                break;
                            case EffectType.Frostbite:
                                actor.stat.moveSpeed = 0;
                                break;
                            case EffectType.Poison:
                                changeValue = actor.stat.entireHp * effect.effectivePoint;
                                actor.stat.entireHp -= changeValue;
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
                if (effect.isStackable)
                    effect.overlappingCount++;
            }

            switch (effect.type)
            {
                case EffectType.Bleeding:
                {
                    var changeValue = actor.stat.entireHp * effect.effectivePoint;
                    for (var i = effect.duration; (i -= deltaTime) > 0;)
                    {
                        HandleDotStatusEffect(actor, effect, changeValue, deltaTime);
                        i -= deltaTime;
                    }
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
                    for (var i = effect.duration; (i -= deltaTime) > 0;)
                    {
                        HandleDotStatusEffect(actor, effect, changeValue, deltaTime);
                        i -= deltaTime;
                    }
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
                    var changeValue = actor.stat.entireHp * effect.effectivePoint;
                    for (var i = effect.duration; (i -= deltaTime) > 0;)
                    {
                        HandleDotStatusEffect(actor, effect, changeValue, deltaTime);
                        i -= deltaTime;
                    }
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private static void HandleDotStatusEffect(Actor actor, Effect effect, float damagePerSecond, float deltaTime)
        {
            var damage = damagePerSecond * deltaTime;

            actor.stat.currentHp -= damage;
            effect.duration -= deltaTime;
        }
        private IEnumerator EndStatusEffect(Effect effect, Actor actor)
        {
            yield return new WaitForSeconds(effect.duration);

            if (!effects.Exists(e => e.type == effect.type)) yield break;
            // 스탯 변경량 원상복구
            switch (effect.type)
            {
                case EffectType.Blind:
                {
                    actor.stat.accuracyRate = actor.stat.baseStat.speed * 0.01f + 0.5f;
                    break;
                }
                case EffectType.Confuse:
                {
                    /*
                     * if(player)
                     *  조작 정상화
                     * if(enemy)
                     *  공격 타겟 정상화
                     */
                    break;
                }
                case EffectType.Frostbite:
                {
                    var changeValue = effect.effectivePoint;
                    actor.stat.moveSpeed += changeValue;
                    break;
                }
                case EffectType.Paralysis:
                {
                    var changeValue = effect.effectivePoint;
                    actor.stat.atkSpeed += changeValue;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (effect.isStackable)
                effect.overlappingCount--;
            if (!effect.isPermanent)
                effects.Remove(effect);
        }
    }
}
    