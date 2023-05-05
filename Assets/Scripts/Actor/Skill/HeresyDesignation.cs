using System.Collections.Generic;
using UnityEngine;

namespace Actor.Skill
{
    public class HeresyDesignation : EffectSkillObject
    {
        public HeresyDesignation(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 60.0f;
            skill.name = "HeresyDesignation";
            skill.type = TargetType.Single;
            skill.elementalType = ElementalType.DeBuff;
            skill.range = 0;
            skill.ultimate = true;
            skill.effectiveSpeed = 1.0f;
            skill.effectivePoint = 30.0f; // 퍼센트
            skill.effectiveDuration = 5.0f;
        }
        /*
         * 혼란, 마비, 부패 중 1개 추가 부여
         */
        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime -= 5.0f;
            this.data.effectiveDuration++;
            this.data.effectivePoint += 10.0f;
        }
    }
}