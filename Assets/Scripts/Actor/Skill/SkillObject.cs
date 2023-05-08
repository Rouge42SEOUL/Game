using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        protected GameObject context;
        protected bool isUnlocked = false;
        [SerializeField] protected Skill data;
        public SkillType type;
        public ElementalType elementalType;

        public SkillObject(GameObject context)
        {
            this.context = context;
        }
        public abstract void Use();

        public void UnlockSkill()
        {
            isUnlocked = true;
        }
    }
}