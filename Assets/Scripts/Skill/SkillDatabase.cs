using UnityEngine;

namespace Skill
{
    [CreateAssetMenu(fileName = "New Skill Database", menuName = "Scriptable Object/Skill/Database")]
    public class SkillDatabase : ScriptableObject
    {
        public SkillObject[] skillObjects;

        private void OnValidate()
        {
            for (int i = 0; i < skillObjects.Length; i++)
            {
                if (skillObjects[i] == null)
                    continue;
                skillObjects[i].Id = i;
            }
        }
    }
}