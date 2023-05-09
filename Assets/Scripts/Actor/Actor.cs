using System;
using System.Collections;
using Actor.Stats;
using Interface;
using UnityEngine;

namespace Actor
{
    // Values or methods that other can use
    public abstract partial class Actor
    {
        public abstract void GetHit();
        public void GetDotDamage(float duration)
        {
            StartCoroutine(AddDotDamage(duration));
        }
        
        public abstract void GetEffect(Effect effect, Func<int, int> getValueToAdd);
        protected abstract void Died();
    }
    
    // Values or methods that other cannot use
    public abstract partial class Actor
    {
        private WaitForSeconds _waitForOneSeconds = new WaitForSeconds(1f);
    }
    
    // body of MonoBehaviour
    public abstract partial class Actor : MonoBehaviour, IDamageable
    {
    }
    
    // body of others
    public abstract partial class Actor
    {
        private IEnumerator AddDotDamage(float duration)
        {
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return _waitForOneSeconds;
                GetHit();
            }
        }
    }
}


