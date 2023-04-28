using UnityEngine;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        public override void GetHit() => _GetHit();
    }
    
    // Values or methods that other cannot use
    public partial class Enemy
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _playerPos;

        [SerializeField] private float distanceWithPlayer = 0.5f;
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor
    {
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
        }

        private void Start()
        {
            _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        }
        
        private void FixedUpdate()
        {
            MovePos();
        }
    }
    
    // body of others
    public partial class Enemy
    {
        protected override void MovePos()
        {
            float dis = Vector2.Distance(_playerPos.position, transform.position);
            if (dis > distanceWithPlayer)
            {
                Vector2 pos = Vector3.Normalize(_playerPos.position - transform.position);
                _rigidbody2D.MovePosition(_rigidbody2D.position + stat.speed * Time.fixedDeltaTime * pos);
            }
        }

        private void _GetHit()
        {
            throw new System.NotImplementedException();
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }
    }
}