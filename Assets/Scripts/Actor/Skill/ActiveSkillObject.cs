using System.Collections.Generic;
using UnityEngine;

namespace Actor.Skill
{
    public class ActiveSkillObject : SkillObject
    {
        [SerializeField] protected TargetType targetType;
        
        public ActiveSkillObject(GameObject context) : base(context)
        {
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }

        protected void GetTarget(out GameObject target)
        {
            target = context;
        }
        
        protected void GetTarget(out List<GameObject> targets)
        {
            targets = new List<GameObject>();
        }
    }
}