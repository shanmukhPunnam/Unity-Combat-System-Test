using UnityEngine;
using Subvrsive.Combat.Bullets;
using Subvrsive.Combat.Characters;
using Subvrsive.Combat.Pool;

namespace Subvrsive.Combat.Weapons
{
    public class Gun : MonoBehaviour
    {
        public Transform firePoint;
        public WeaponData weaponData;

        private float lastFireTime;

        public void Initialize(WeaponData data)
        {
            weaponData = data;
        }

        public void TryShoot(CharacterHealth target , CharacterManager attacker)
        {
            if (Time.time - lastFireTime < weaponData.fireRate) return;
            if (target == null || !target.IsAlive()) return;

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > weaponData.range) return;

            Fire(target, attacker);
            lastFireTime = Time.time;
        }

        private void Fire(CharacterHealth target, CharacterManager attacker)
        {
            GameObject bulletObj = ObjectPool.Instance.SpawnFromPool(weaponData.bulletPrefab.name, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.Initialize(target.transform, weaponData, attacker);
        }
    }
}
