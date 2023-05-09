using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actor.Skill
{
    [Serializable]
    public class SkillSlot
    {
        public SkillType slotType;
        [SerializeField] private SkillObject _skillObject;

        public Action<SkillObject> OnChangeSkill;

        public SkillSlot()
        {
            slotType = SkillType.Active;
            _skillObject = null;
        }

        public void UpdateSlot(SkillObject skillObject)
        {
            if (skillObject.type == slotType)
            {
                _skillObject = skillObject;
                OnChangeSkill?.Invoke(_skillObject);
                return;
            }
        }

        public void UseSkill()
        {
            Debug.Log("call SkillObject.Use");
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