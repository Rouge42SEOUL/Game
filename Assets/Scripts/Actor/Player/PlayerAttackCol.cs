
using Interface;
using UnityEngine;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class PlayerAttackCol
    {
        public DamageData DmgData;
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
                // TOD0 : optimize and reimplement KnockBackForce power
                DmgData.KbForce = Vector3.Normalize(other.transform.position - _player.transform.position);
                other.GetComponent<Enemy.Enemy>().GetHit(DmgData);
            }
        }
    }
    
    // body of others
    public partial class PlayerAttackCol
    {
        
    }
}
