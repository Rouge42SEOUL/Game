using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Stats
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Stat/EnemyStat")]
    public class EnemyStatObject : ActorStatObject
    {

        protected override void OnEnable()
        {
            base.OnEnable();
            // base health point initialize
        }
        
        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
        }
    }
}