using Actor.Enemy;
using Elemental;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Stat/EnemyStat")]
    public class EnemyStatObject : ActorStatObject
    {
        public EnemyAttackType attackType;
        public float attackRange;
        
        protected override void InitElementalType()
        {
            elementalType = ElementalType.Normal;
        }

        protected override void OnEnable()
        {
            if (isInitialized)
                return;
            
            base.OnEnable();
            attackType = EnemyAttackType.Collision;
            attackRange = 1.0f;
        }
    }
}