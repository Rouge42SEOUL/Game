
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        public override void GetHit() => _GetHit();
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _movement;
    }
    
    // body of MonoBehaviour
    public partial class Player : Actor
    {
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            MovePos();
        }
    }
    
    // body of others
    public partial class Player
    {
        protected override void MovePos()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + stat.speed * Time.fixedDeltaTime * _movement);
        }

        private void _GetHit()
        {
            throw new System.NotImplementedException();
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }

        private void OnMovement(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
    }
}
