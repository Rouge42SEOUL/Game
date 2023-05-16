using System;
using System.Collections.Generic;
using Actor.Stats;
using Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Skill
{
    [Serializable]
    public class SkillSlot
    {
        public SkillType slotType;
        [SerializeField] private SkillObject _skillObject;

        public Action OnChangeSkill;

        public void OnAttackTrigger(GameObject target)
        {
            if (_skillObject is AttackSkillObject)
            {
                AttackSkillObject skill = (AttackSkillObject)_skillObject;
                skill.OnAttackTrigger(target);
            }
        }

        public SkillSlot()
        {
            slotType = SkillType.Active;
            _skillObject = null;
        }

        public void SetContext(IActorContext context)
        {
            if (_skillObject == null)
                return;
            _skillObject.context = context;
        }
        
        public void UpdateSlot(SkillObject skillObject, IActorContext context)
        {
            if (skillObject == null)
                return;
            
            if (skillObject.type == slotType)
            {
                _skillObject = skillObject;
                _skillObject.context = context;
                OnChangeSkill?.Invoke();
            }
        }

        public void UseSkill()
        {
            Debug.Log("SkillObject.Use: " + _skillObject.name);
            if (_skillObject == null)
                return;
            _skillObject.Use();
        }

        public void CancelSkill()
        {
            if (_skillObject == null)
                return;
            _skillObject.Cancel();
        }
    }
}