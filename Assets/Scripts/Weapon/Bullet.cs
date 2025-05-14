using UnityEngine;
using Subvrsive.Combat.Characters;

namespace Subvrsive.Combat.Bullets
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        private int damage;
        private Transform target;

        public void Initialize(Transform targetTransform, int bulletDamage)
        {
            target = targetTransform;
            damage = bulletDamage;
            Invoke(nameof(Deactivate), 3f);
        }

        void Update()
        {
            if (target == null)
            {
                gameObject.SetActive(false);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                var health = target.GetComponent<CharacterHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }

                gameObject.SetActive(false);
            }
        }

        private void Deactivate()
        {
            ObjectPool.Instance.ReturnToPool(gameObject.name, gameObject);
            //gameObject.SetActive(false);
        }
    }
}
