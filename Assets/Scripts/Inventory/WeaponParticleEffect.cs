using Subvrsive.Combat.Pool;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponParticleEffect", menuName = "Inventory/Weapon Particle Effect")]
public class WeaponParticleEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] private GameObject particleEffectPrefab;
    public void PlayEffect(Transform target)
    {
        // spawn particle effects on hit
        ObjectPool.Instance.SpawnFromPool(particleEffectPrefab.name, target.position, Quaternion.identity);
    }


}
