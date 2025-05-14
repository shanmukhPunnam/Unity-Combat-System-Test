using Subvrsive.Combat.Event;
using Subvrsive.Combat.Manager;
using UnityEngine;

namespace Subvrsive.Combat.Characters
{
    public class CharacterManager : MonoBehaviour
    {
        public float TargetDistance = 10f; // Distance to the target enemy
        [SerializeField] bool canShootEnimy = false;


        public CharacterMovement characterMovement;
        public CharacterHealth characterHealth;
        public WeaponHandler WeaponHandler;
        public CharacterStats characterStats;

        private CharacterManager enemyCharacter;

        bool isMatchOver = false;


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

            if (WeaponHandler == null)
            {
                WeaponHandler = GetComponent<WeaponHandler>();
            }

            if (characterStats == null)
            {
                characterStats = GetComponent<CharacterStats>();
            }

            EventManager.OnGameOver += OnGameOver;
        }

        private void OnDestroy()
        {
            EventManager.OnGameOver -= OnGameOver;
        }

        void OnGameOver()
        {
            isMatchOver = true;
        }

        private void Update()
        {
            if (isMatchOver == true)
            {
                return;
            }

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
}


