using System;
using System.Collections.Generic;
using Actor.Skill;
using Core;
using UnityEngine;

namespace Actor.Stats
{
    public abstract class ActorStatObject : ScriptableObject
    {
        protected bool isInitialized = false;
        
        public AttackSkillObject normalAttack;
        public SerializableDictionary<AttributeType, Attribute> baseAttributes = new();
        public List<Effect> effects = new();
        public ElementalType elementalType;
        
        public float baseHealthPoint;

        protected virtual void OnEnable()
        {
            if (isInitialized)
                return;
            isInitialized = true;
            baseAttributes.Clear();
            // TODO: set initial stats
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                baseAttributes[type] = new Attribute(type, 10);
            }
            CalculateSideAttributes();
            effects.Clear();
        }

        protected void CalculateSideAttributes()
        {
            // TODO: calculate 
            baseHealthPoint = baseAttributes[AttributeType.Health].value * 10;
        }

        public void AddEffect(Effect effect, Actor<ActorStatObject> target)
        {
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
                                changeValue = effect.effectValue;
                                baseHealthPoint -= changeValue;
                                break;
                            case EffectType.Frostbite:
                                target.stat.baseAttributes[AttributeType.MoveSpeed].value = 0;
                                break;
                            case EffectType.Poison:
                                changeValue = target.stat.entireHp * effect.effectivePoint;
                                target.stat.entireHp -= changeValue;
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
                    var changeValue = target.stat.entireHp * effect.effectivePoint;
                    for (var i = effect.duration; (i -= deltaTime) > 0;)
                    {
                        HandleDotStatusEffect(actor, effect, changeValue, deltaTime);
                        i -= deltaTime;
                    }
                    break;
                }
                case EffectType.Blind:
                {
                    target.stat.accuracyRate = 0;
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
                    target.stat.moveSpeed -= changeValue;
                    break;
                }
                case EffectType.Frostbite:
                {
                    var changeValue = effect.effectivePoint;
                    target.stat.moveSpeed -= changeValue;
                    break;
                }
                case EffectType.Paralysis:
                {
                    var changeValue = effect.effectivePoint;
                    target.stat.atkSpeed -= changeValue;
                    break;
                }
                case EffectType.Poison:
                {
                    var changeValue = target.stat.entireHp * effect.effectivePoint;
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

        }
    }
}