using System;
using Actor.Stats;
using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill Database", menuName = "Skill/Database")]
    public class SkillDatabase : ScriptableObject
    {
        public SkillObject[] skillObjects;

        private void OnValidate()
        {
            for (int i = 0; i < skillObjects.Length; i++)
            {
                skillObjects[i].Id = i;
            }
        }
    }
}