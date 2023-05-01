using System;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

namespace Actor.Enemy
{
    // Values or methods that other can use
    public partial class Enemy
    {
        protected StateMachine<Enemy> stateMachine;
        
        internal GameObject Target => _target;
        internal ActorStatObject Stat => stat;
        // public Dictionary<ActorStatType, int> CurrentStat { get; protected set; }
        
        public override void GetHit() => _GetHit();

        internal bool IsAttackable
        {
            get
            {
                float dis = Vector2.Distance(_target.transform.position, transform.position);
                return attackableDistance >= dis;
            }
        }
    }
    
    // Values or methods that other cannot use
    public partial class Enemy
    {
        private GameObject _target;
        [SerializeField] private float attackableDistance = 0.5f;
    }
    
    // body of MonoBehaviour
    public partial class Enemy : Actor
    {
        // private void OnEnable()
        // {
        //    CurrentStat[ActorStatType.Health] = stat.health;
        //    CurrentStat[ActorStatType.Attack] = stat.atkPower;
        //    CurrentStat[ActorStatType.Speed] = stat.speed;
        // }

        private void Start()
        {
            _target = GameObject.Find("Player");
            stateMachine = new StateMachine<Enemy>(this, new IdleState());
            stateMachine.AddState(new MoveState());
            stateMachine.AddState(new AttackState());
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
    }
    
    // body of others
    public partial class Enemy
    {
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