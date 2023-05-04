using Unity.VisualScripting;
using UnityEngine;

namespace Actor.Skill
{
    public abstract class EffectSkillObject : SkillObject
    {
        [SerializeField] private LayerMask targetLayer; // 대상 레이어 마스크
        public EffectSkillObject(GameObject context, Skill skill) : base(context)
        {}
        
        public override void Use()
        {
            float _range = this.data.range;

            Transform transform = this.user.gameObject.GetComponent<Transform>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, _range, targetLayer); // 범위 내의 대상 검색
            if (this.data.type == (TargetType)0)
            {
                // this.user.gameObject.GetComponent<Actor>().TakeEffect(this.data);
            }
            else if (this.data.type == (TargetType)1)
            {
                foreach (Collider collider in colliders)
                {
                    // collider.gameObject.GetComponent<Actor>().TakeEffect(this.data);
                }
            }
            else
            {
                if (colliders.Length > 0)
                {
                    Collider closestCollider = GetClosestCollider(colliders, transform); // 가장 가까운 대상 검색
                    if (closestCollider != null)
                    {
                        GameObject closestObject = closestCollider.gameObject;
                        // closestObject.GetComponent<Actor>().TakeEffect(this.data);
                    } 
                }
            }
        }

        public abstract override void LevelUp();


        private Collider GetClosestCollider(Collider[] colliders, Transform transform)
        {
            Collider closestCollider = null;
            float closestDistance = Mathf.Infinity;
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestCollider = collider;
                    closestDistance = distance;
                }
            }
            return closestCollider;
        }
    }
}
