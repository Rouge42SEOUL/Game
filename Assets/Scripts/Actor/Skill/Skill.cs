
using UnityEngine;
using System;

namespace Actor.Skill
{
    [Serializable]
    public struct Skill
    {
        public int level; //skill_level(합계산)
        public float coolTime;// cool_time(퍼센트 계산)
        public TargetType type; // 스킬 타겟팅범위
        public string name;// 이름
        public ElementalType elementalType; //속성
        public float range; //사거리
        public bool ultimate; //궁극기
        public float effectivePoint; //효과량
        public float effectiveSpeed; // 시전속도
        public float effectiveDuration; //효과지속시간
    }
}
