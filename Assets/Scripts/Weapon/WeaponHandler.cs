using UnityEngine;
using Subvrsive.Combat.Weapons;
using Subvrsive.Combat.Characters;

namespace Subvrsive.Combat.Characters
{
    public class WeaponHandler : MonoBehaviour
    {
        public Transform handSocket;
        public WeaponInventory weaponInventory;

        private Gun currentGun;

        void Start()
        {
            EquipRandomWeapon();
        }

        void EquipRandomWeapon()
        {
            if (weaponInventory == null || weaponInventory.weapons.Count == 0) return;

            int index = Random.Range(0, weaponInventory.weapons.Count);
            var weaponData = weaponInventory.weapons[index];

            GameObject weaponObj = Instantiate(weaponData.weaponPrefab, handSocket.position, handSocket.rotation, handSocket);
            currentGun = weaponObj.GetComponent<Gun>();
        }

        public void TryShoot(CharacterHealth target)
        {
            currentGun?.TryShoot(target);
        }

        public bool CanShoot(CharacterHealth target)
        {
            if (target == null || !target.IsAlive()) return false;

            Vector3 rayOrigin = transform.position + new Vector3(0, 0.5f, 0); 
            Vector3 direction = (target.transform.position - rayOrigin).normalized;
            float distance = Vector3.Distance(rayOrigin, target.transform.position);

            Debug.DrawRay(rayOrigin, direction * distance, Color.red); // Always draw ray

            if (Physics.Raycast(rayOrigin, direction, out RaycastHit hit, distance))
            {
                if (hit.transform == target.transform)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
