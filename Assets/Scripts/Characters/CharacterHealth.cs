using Subvrsive.Combat.Manager;
using System;
using UnityEngine;

namespace Subvrsive.Combat.Characters
{
    public class CharacterHealth : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;


        public event Action OnDeath;
        public event Action<float> OnHealthChanged;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public bool IsAlive()
        {
            return currentHealth > 0;
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive()) return;

            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.Instance.liveCharacters.Remove(gameObject);
                OnDeath?.Invoke();
                gameObject.SetActive(false);
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive()) return;

            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnHealthChanged?.Invoke(currentHealth);
        }

        public void ResetHealth()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }
}
