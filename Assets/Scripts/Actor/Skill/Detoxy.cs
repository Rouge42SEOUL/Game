using UnityEngine;

namespace Actor.Skill
{
    public class Detoxy : EffectSkillObject
    {
        public Detoxy(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 10.0f;
            skill.name = "Detoxy";
            skill.type = TargetType.Self;
            skill.elementalType = ElementalType.Buf;
            skill.range = 0;
            skill.ultimate = false;
            skill.effectiveSpeed = 0.1f;
            skill.effectivePoint = 0.0f;
            skill.effectiveDuration = 0.1f;
        }
        
        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime -= 1.0f;
            this.data.effectiveDuration += 0.1f ;
        }
    }
}