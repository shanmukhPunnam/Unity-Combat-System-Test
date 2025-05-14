using Subvrsive.Combat.Bullets;
using Subvrsive.Combat.Weapons;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;       // The name of the weapon
    public int damage;              // The damage the weapon does
    public float range;             // The range of the weapon
    public float fireRate;          // The rate of fire of the weapon

    public GameObject weaponPrefab;   // The gun model
    public GameObject bulletPrefab;   // The bullet to shoot

    public ScriptableObject weaponEffect; // The effect to play when shooting
}