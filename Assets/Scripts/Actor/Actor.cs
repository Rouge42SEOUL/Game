
using Interface;
using UnityEngine;

namespace Actor
{
    // Values or methods that other can use
    public abstract partial class Actor
    {
        [SerializeField]
        protected ActorStatObject stat;

        public abstract void GetHit();
        protected abstract void Died();
    }
    
    // Values or methods that other cannot use
    public abstract partial class Actor
    {
        
    }
    
    // body of MonoBehaviour
    public abstract partial class Actor : MonoBehaviour, IDamageable
    {
        
    }
    
    // body of others
    public abstract partial class Actor
    {
        
    }
}


