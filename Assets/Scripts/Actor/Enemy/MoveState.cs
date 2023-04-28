using System.Collections;
using System.Collections.Generic;
using Actor.Enemy;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    
}
public class MoveState : State<Enemy>
{
    private Rigidbody2D _rigidbody2D;
    private Transform _playerPos;

    public override void OnInitialized()
    {
        _rigidbody2D = _context.GetComponent<Rigidbody2D>();
        _playerPos = _context.Target.transform;
    }

    public override void Update()
    {}

    public override void FixedUpdate()
    {
        if (!_context.IsAttackable)
        {
            Vector2 pos = Vector3.Normalize(_playerPos.position - _context.transform.position);
            _rigidbody2D.MovePosition(_rigidbody2D.position + _context.Stat.speed * Time.fixedDeltaTime * pos);
        }
        else
        {
            _stateMachine.ChangeState<AttackState>();
        }
    }
}
