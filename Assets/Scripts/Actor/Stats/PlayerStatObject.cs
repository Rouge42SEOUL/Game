using Actor.Skill;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Stat/PlayerStat")]
    public class PlayerStatObject : ActorStatObject
    {
        #region Variables
        
        public SkillSlot[] skills = new SkillSlot[4];
        public SkillObject passive;
        
        private int _level = 1;
        private int _exp = 0;

        private int _baseHealthPoint;
        private int _currentHealthPoint;

        #endregion

        #region Properties

        public int Level => _level;
        public float PercentHealPoint => (_baseHealthPoint > 0) ? (_currentHealthPoint / _baseHealthPoint) : 0;

        #endregion

        #region PrivateMethods

        protected override void OnEnable()
        {
            if (!isInitialized)
                return;
            
            base.OnEnable();
            skills[3].slotType = SkillType.Ultimate;
            _baseHealthPoint = (int)baseAttributes[AttributeType.Health].value * 10;
            _currentHealthPoint = _baseHealthPoint;
        }

        #endregion
        
        #region PublicMethods

        public void AddExp(int value)
        {
            this._exp += value;
            // if exp > max, level++ 
            // add attribute base value by level
        }

        #endregion
    }
}