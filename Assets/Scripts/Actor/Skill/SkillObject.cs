using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        private GameObject _context;
        [SerializeField] public Skill data;
        [SerializeField] public Actor user;
        public bool isUnlocked = false;

        public SkillObject(GameObject context)
        {
            this._context = context;
        }

        public SkillObject(GameObject context, Skill data)
        {
            this._context = context;
            this.data = data;
        }
        public abstract void Use();
        public abstract void LevelUp();
    }
}