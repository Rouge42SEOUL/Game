using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        [SerializeField] protected Skill data;
        protected GameObject context;
        protected bool isUnlocked = false;
        
        public SkillType type;
        public ElementalType elementalType;

        public int Id
        {
            get => data.id;
            set => data.id = value;
        }
        
        public SkillObject(GameObject context)
        {
            this.context = context;
        }
        public abstract void Use();

        public abstract void Cancel();

        public void UnlockSkill()
        {
            isUnlocked = true;
        }

        public string GetName() => data.name;
        public string GetDescription() => data.description;
    }
}