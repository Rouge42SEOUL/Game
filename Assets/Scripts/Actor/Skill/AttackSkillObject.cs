using System.Collections.Generic;
using Actor.Enemy;
using UnityEngine;

namespace Actor.Skill
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Attack")]
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
                {
                    GameObject target = GetTarget();
                    target.GetComponent<Actor>().GetHit();
                    break;
                }
                case TargetType.Area:
                {
                    List<GameObject> targets;
                    GetTarget(out targets);
                    foreach (var target in targets)
                        target.GetComponent<Actor>().GetHit();
                    break;
                }
                case TargetType.World:
                {
                    // get all enemies from spawner
                    break;
                }

            }
        }

        public override void Cancel()
        {
            
        }
    }
}