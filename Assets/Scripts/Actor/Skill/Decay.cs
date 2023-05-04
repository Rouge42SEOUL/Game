using UnityEngine;

namespace Actor.Skill
{
    public class Decay : EffectSkillObject
    {
        public Decay(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 10.0f;
            skill.name = "Decay";
            skill.type = TargetType.Single;
            skill.elementalType = ElementalType.Debuf;
            skill.range = 0;
            skill.ultimate = false;
            skill.effectiveSpeed = 1.0f;
            skill.effectivePoint = 30.0f; // 퍼센트
            skill.effectiveDuration = 5.0f;
        }

        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime -= 0.5f;
            this.data.effectiveDuration++;
            this.data.effectivePoint += 10.0f;
        }
    }
}