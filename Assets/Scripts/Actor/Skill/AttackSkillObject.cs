using System.Collections.Generic;
using Actor.Enemy;
using UnityEngine;

namespace Actor.Skill
{
    public class AttackSkillObject : ActiveSkillObject
    {
        [SerializeField] private TargetType _type;
        [SerializeField] private float _range;

        public AttackSkillObject(GameObject context) : base(context)
        {
            _type = TargetType.Projectile;
        }

        public AttackSkillObject(GameObject context, float range) : base(context)
        {
            _type = TargetType.Area;
            _range = range;
        }
        
        public override void Use()
        {
            switch (_type)
            {
                case TargetType.Projectile:
                    GameObject target = null;
                    Attack(target);
                    break;
                case TargetType.Area:
                    var targets = new List<GameObject>();
                    Attack(ref targets);
                    break;
                case TargetType.World:
                    Attack();
                    break;
            }
            
        }
        private void Attack()
        {
            
        }
        

        public override void Cancel()
        {
            
        }

        private void Attack(GameObject target)
        {
            
        }
        
        private void Attack(ref List<GameObject> targets)
        {
            if (targets.Count == 0)
                return;
            foreach (var target in targets)
            {
                target.GetComponent<Actor>().GetHit();
            }
        }
    }
}