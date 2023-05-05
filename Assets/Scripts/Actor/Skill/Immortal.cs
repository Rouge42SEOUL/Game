using UnityEngine;

namespace Actor.Skill
{
    public class Immortal : EffectSkillObject
    {
        public Immortal(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 60.0f;
            skill.name = "Immortal";
            skill.type = TargetType.Self;
            skill.elementalType = ElementalType.Buff;
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
