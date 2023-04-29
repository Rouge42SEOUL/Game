using UnityEngine;

namespace Actor.Skill
{
    public abstract class SkillObject : ScriptableObject
    {
        private GameObject _context;
        [SerializeField] private Skill _data;
        [SerializeField] private ElementalType elementalType;
        public bool isUnlocked = false;

        public SkillObject(GameObject context)
        {
            this._context = context;
        }
        public abstract void Use();
    }
}