using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Skill
{
    [Serializable]
    public class SkillSlot
    {
        [SerializeField] public List<SkillType> possibleTypes = new List<SkillType>();
        [SerializeField] private SkillObject _skillObject;

        public Action<SkillObject> OnChangeSkill;

        public SkillSlot()
        {
            _skillObject = null;
            possibleTypes.Add(SkillType.Active);
        }

        public void UpdateSlot(SkillObject skillObject)
        {
            foreach (var type in possibleTypes)
            {
                if (skillObject.type == type)
                {
                    _skillObject = skillObject;
                    OnChangeSkill?.Invoke(_skillObject);
                    return;
                }
            }
        }
    }
}