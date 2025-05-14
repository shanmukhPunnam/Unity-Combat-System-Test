using Subvrsive.Combat.Characters;
using Subvrsive.Combat.Manager;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float TargetDistance = 10f; // Distance to the target enemy
    [SerializeField] bool canShootEnimy = false;


    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] CharacterHealth characterHealth;
    [SerializeField] WeaponHandler WeaponHandler;

    public CharacterManager enemyCharacter;

    void Start()
    {
        if (characterMovement == null)
        {
            characterMovement = GetComponent<CharacterMovement>();
        }

        if (characterHealth == null) 
        {
            characterHealth = GetComponent<CharacterHealth>();
        }

        if(WeaponHandler == null)
        {
            WeaponHandler = GetComponent<WeaponHandler>();
        }
    }

    private void Update()
    {
        if (enemyCharacter == null || enemyCharacter.characterHealth.IsAlive() == false)
        {
            //Get New Enimy
            GameObject enimy = GameManager.Instance.GetRandomLiveEnimy();
            if (enimy != null && enimy != gameObject)
            {
                enemyCharacter = enimy.GetComponent<CharacterManager>();
            }
            else
            {
                enemyCharacter = null;
            }
        }

        if (enemyCharacter != null && enemyCharacter.characterHealth.IsAlive())
        {

            canShootEnimy = WeaponHandler.CanShoot(enemyCharacter.characterHealth);
            if (canShootEnimy == true)
            {
                Shoot();
            }
            else
            {
                characterMovement.targetPosition = enemyCharacter.transform;
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        characterMovement.targetPosition = enemyCharacter.transform; 
    }


    [ContextMenu("Shoot Enimy")]
    public void Shoot()
    {
        if (enemyCharacter != null && enemyCharacter.characterHealth.IsAlive())
        {
            WeaponHandler.TryShoot(enemyCharacter.characterHealth);
        }
    }
}
