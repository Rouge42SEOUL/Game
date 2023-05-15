using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Stat/EnemyStat")]
    public class EnemyStatObject : ActorStatObject
    {
        private int _baseHealthPoint;
        
        protected override void OnEnable()
        {
            if (!isInitialized)
                return;
            
            base.OnEnable();
            _baseHealthPoint = (int)baseAttributes[AttributeType.Health].value * 10;
        }
        
        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
        }
    }
}