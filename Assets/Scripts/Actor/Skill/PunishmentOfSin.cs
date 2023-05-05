using UnityEngine;

namespace Actor.Skill
{
    public class PunishmentOfSin : EffectSkillObject
    {
        public PunishmentOfSin(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 60.0f;
            skill.name = "PunishmentOfSin";
            skill.type = TargetType.Single;
            skill.elementalType = ElementalType.DeBuff;
            skill.range = 0;
            skill.ultimate = true;
            skill.effectiveSpeed = 1.0f;
            skill.effectivePoint = 30.0f; // 퍼센트
            skill.effectiveDuration = 5.0f;
        }

        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime -= 5.0f;
            this.data.effectiveDuration++;
            this.data.effectivePoint += 10.0f;
        }
    }
}