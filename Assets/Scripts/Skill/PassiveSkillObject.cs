using Actor.Stats;
using UnityEngine;
using Elemental;
using Items;

namespace Skill
{
    [CreateAssetMenu(fileName = "New Passive Skill", menuName = "Scriptable Object/Skill/Passive")]
    public class PassiveSkillObject : ScriptableObject
    {
        [SerializeField] public ElementalType elementalType; // passive 속성
        [SerializeField] public WeaponType weaponType; //특정 장비 착용에 따른 보정
        [SerializeField] public ElementalType harmonElementalType;//skill의 보정치 받는 속성
        [SerializeField] public ElementalType oppositeElementalType;//skill의 역보정 받는 속성
        [SerializeField] public float discount; // 재화 이벤트시 가격 할인
        [SerializeField] public float statVariation; // 함정발동시 스텟 변동 폭
        [SerializeField] public AttributeType addTo; // 래밸업 스탯 변화량 보정치 받는 스텟 종류
        [SerializeField] public AttributeType subTo; // 래벨업 스텟 변화량 역보정치 받는 스텟 종류
        [SerializeField] public EventType eventType; // 특정 이벤트시 보정
        
    }
}