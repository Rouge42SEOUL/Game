using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        private GameObject _context;
        [SerializeField] private Skill _data;
        public SkillType type;
        public ElementalType elementalType;
        private bool isUnlocked = false;

        public SkillObject(GameObject context)
        {
            this._context = context;
        }
        public abstract void Use();

        public abstract void Cancel();

        public void UnlockSkill()
        {
            isUnlocked = true;
        }
    }
}