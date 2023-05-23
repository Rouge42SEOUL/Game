using Actor.Enemy;
using Elemental;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Stat/EnemyStat")]
    public class EnemyStatObject : ActorStatObject
    {
        protected override void InitElementalType()
        {
            elementalType = ElementalType.None;
        }

        protected override void OnEnable()
        {
            if (isInitialized)
                return;
            
            base.OnEnable();
        }
    }
}