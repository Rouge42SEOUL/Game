using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Stat/EnemyStat")]
    public class EnemyStatObject : ActorStatObject
    {
        protected override void OnEnable()
        {
            if (!isInitialized)
                return;

            base.OnEnable();
        }

        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
        }
    }
}