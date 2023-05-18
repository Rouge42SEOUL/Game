using Elemental;
using Interface;
using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        protected IActorContext context;
        public SkillType type;
        public ElementalType elementalType;
        public int level;
        
        [SerializeField] protected Skill data;
        protected bool isUnlocked = false;
        
        public int Id
        {
            get => data.id;
            set => data.id = value;
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