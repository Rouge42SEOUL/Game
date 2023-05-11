
using System;
using Actor.Stats;
using Interface;
using UnityEngine;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class PlayerAttackCol
    {
        public DamageData DmgData;
        public Action<GameObject> OnAttackTrigger;
    }
    
    // Values or methods that other cannot use
    public partial class PlayerAttackCol
    {
        private Player _player;
    }

    // body of MonoBehaviour
    public partial class PlayerAttackCol : MonoBehaviour
    {
        private void Awake()
        {
            _player = transform.parent.GetComponent<Player>();
            DmgData.Damage = 5;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                OnAttackTrigger?.Invoke(other.gameObject);
            }
        }
    }
    
    // body of others
    public partial class PlayerAttackCol
    {
        
    }
}
