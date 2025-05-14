using UnityEngine;
using Subvrsive.Combat.Characters;
using Subvrsive.Combat.Pool;

namespace Subvrsive.Combat.Bullets
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        private int damage;
        private Transform target;
        CharacterManager attackerCharacter;
        WeaponData weaponData;

        public void Initialize(Transform targetTransform, WeaponData weaponData , CharacterManager attacker)
        {
            this.weaponData = weaponData;   
            this.target = targetTransform;
            this.damage = weaponData.damage;
            Invoke(nameof(Deactivate), 3f);
            this.attackerCharacter = attacker;
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
                    health.TakeDamage(damage, attackerCharacter);
                    if(weaponData.weaponEffect != null)
                    {
                        if (weaponData.weaponEffect is IWeaponEffect weaponEffect)
                        {
                            weaponEffect.PlayEffect(target);
                        }
                    }
                }

                gameObject.SetActive(false);
            }
        }

        private void Deactivate()
        {
            ObjectPool.Instance.ReturnToPool(gameObject.name, gameObject);
        }
    }
}
