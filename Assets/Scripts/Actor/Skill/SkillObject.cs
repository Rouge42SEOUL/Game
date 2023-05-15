using System;
using Actor.Stats;
using Interface;
using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        public IActorContext context;
        public SkillType type;
        public ElementalType elementalType;
        
        [SerializeField] protected Skill data;
        protected bool isUnlocked = false;
        
        public int Id
        {
            get => data.id;
            set => data.id = value;
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