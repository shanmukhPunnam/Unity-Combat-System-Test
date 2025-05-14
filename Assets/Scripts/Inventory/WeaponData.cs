using Subvrsive.Combat.Bullets;
using Subvrsive.Combat.Weapons;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float range;
    public float fireRate;

    public GameObject weaponPrefab;   // The gun model
    public GameObject bulletPrefab;   // The bullet to shoot


}