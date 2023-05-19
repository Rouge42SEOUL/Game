using Elemental;
using Interface;
using UnityEngine;

namespace Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        protected IActorContext context;
        public SkillType type;
        public ElementalType elementalType;
        public int level;
        
        [SerializeField] protected global::Skill.Skill data;
        protected bool isUnlocked = false;
        
        public int Id
        {
            get
            {
                if (data == null)
                    return -1;
                return data.id;
            }
            set
            {
                if (data == null)
                    data = new global::Skill.Skill();
                data.id = value;
            }
        }

        public IActorContext Context => context;

        public virtual void SetContext(IActorContext actor)
        {
            context = actor;
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