using System;
using Core;
using Elemental;
using Newtonsoft.Json;
using Skill;
using UnityEngine;

namespace Actor.Stats
{
    [JsonObject(MemberSerialization.Fields)]
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Stat/PlayerStat")]
    public class PlayerStatObject : ActorStatObject
    {
        #region Variables
        
        public AttackSkillObject normalAttack;
        public SkillSlot[] skills = new SkillSlot[4];
        public PassiveSkillObject passive;
        
        public SerializableDictionary<EffectType, Effect> effects = new();
        
        private int _level = 1;

        public SerializableDictionary<AttributeType, Attribute> currentAttributes = new();
        public float currentHealthPoint;

        #endregion

        #region Properties

        public int Level => _level;
        public float PercentHealPoint => (baseHealthPoint > 0) ? (currentHealthPoint / baseHealthPoint) : 0;

        #endregion

        #region PrivateMethods
        
        protected override void InitElementalType()
        {
            elementalType = passive ? passive.elementalType : ElementalType.Normal;
        }
        
        #endregion

        #region MonoBehaviourMethods
        protected override void OnEnable()
        {
            if (isInitialized)
                return;
            
            base.OnEnable();
            effects.Clear();
            currentAttributes.Clear();
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                currentAttributes[type] = new Attribute(type, baseAttributes[type].value);
            }
            currentHealthPoint = baseHealthPoint;
        }

        #endregion
        
        #region PublicMethods
        
        public void InitStat()
        {
            isInitialized = false;
            OnEnable();
        }

        public void LevelUp()
        {
            float count;
            this._level++;
            foreach (AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                count = 0;
                switch (type)
                {
                    case AttributeType.Speed:
                        count += 2;
                        baseAttributes[type].value += 2;
                        currentAttributes[type].value += 2;
                        break;
                    case AttributeType.Attack or AttributeType.Health or AttributeType.Defense:
                        count += 3;
                        baseAttributes[type].value += 3;
                        currentAttributes[type].value += 3;
                        break;
                }

                if (passive.addTo == type)
                {
                    count++;
                    baseAttributes[type].value++;
                    currentAttributes[type].value++;
                }

                if (passive.subTo == type)
                {
                    count--;
                    baseAttributes[type].value--;
                    currentAttributes[type].value--;
                }

                switch (type)
                {
                    case (AttributeType.Accuracy or AttributeType.Avoidance):
                        currentAttributes[type].value += count * 0.01f;
                        break;
                    case (AttributeType.AttackSpeed):
                        currentAttributes[type].value += count * 0.015f;
                        break;
                    case (AttributeType.MoveSpeed):
                        currentAttributes[type].value += count * 0.025f;
                        break;
                    case (AttributeType.Health):
                        currentHealthPoint += count * 10f;
                        break;
                }
            }
            CalculateSideAttributes();
        }

        public void AddAttribute(AttributeType type, float value)
        {
            currentAttributes[type].value += value;
        }

        public void SubAttribute(AttributeType type, float value)
        {
            currentAttributes[type].value -= value;
        }

        public void AddEffect(Effect effect)
        {
            
        }

        public void DeleteEffect(EffectType type)
        {
            
        }
        
        #endregion
    }
}