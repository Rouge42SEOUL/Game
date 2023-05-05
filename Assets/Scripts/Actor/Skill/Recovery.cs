using UnityEngine;

namespace Actor.Skill
{
    public class Recovery : EffectSkillObject
    {
        public Recovery(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 30.0f;
            skill.name = "Recovery";
            skill.type = TargetType.Self;
            skill.elementalType = ElementalType.Buf;
            skill.range = 0;
            skill.ultimate = false;
            skill.effectiveSpeed = 0.1f;
            skill.effectivePoint = 0.0f;
            skill.effectiveDuration = 0.1f;
        }
        /*
         * 상태이상 회복
         */
        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime -= 5.0f;
            this.data.effectiveDuration += 0.1f ;
        }
    }
}