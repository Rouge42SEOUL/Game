using UnityEngine;

namespace Actor.Skill
{
    public class Confuse : EffectSkillObject
    {
        public Confuse(GameObject context, Skill skill) : base(context, skill)
        {
            skill.level = 1;
            skill.coolTime = 10.0f;
            skill.name = "Confuse";
            skill.type = TargetType.Single;
            skill.elementalType = ElementalType.DeBuff;
            skill.range = 5.0f;
            skill.ultimate = false;
            skill.effectiveSpeed = 1.0f;
            skill.effectivePoint = 0.0f;
            skill.effectiveDuration = 10.0f;
        }
    
        /*
     * TODO:조작 관련 로직 추가 예정
     */
        public override void LevelUp()
        {
            this.data.level++;
            this.data.coolTime--;
            this.data.effectiveDuration++;
        }
    }
}