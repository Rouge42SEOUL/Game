using UnityEngine;

namespace Actor.Skill
{
    public class DescentOfGod : EffectSkillObject
    {
        public DescentOfGod(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 60.0f;
            skill.name = "DescentOfGod";
            skill.type = TargetType.Self;
            skill.elementalType = ElementalType.Buf;
            skill.range = 0;
            skill.ultimate = true;
            skill.effectiveSpeed = 0.1f;
            skill.effectivePoint = 0.0f;
            skill.effectiveDuration = 3.0f;
        }
        

        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime -= 5.0f;
            this.data.effectiveDuration++;
        }
    }
}