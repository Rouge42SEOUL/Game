using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Actor/Skill/Passive")]
    public class PassiveSkillObject : SkillObject
    {
        public PassiveSkillObject(GameObject context) : base(context)
        {
            UnlockSkill();
        }
        
        public override void Use()
        {
             
        }

        public override void Cancel()
        {
            
        }

    }
}