using Subvrsive.Combat.Characters;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HealthbarUI : MonoBehaviour
{
    public Slider HalthbarSlider;
    public CharacterHealth characterHealth;
    public Vector3 offset = new Vector3(0, 2f, 0);

    public void Initialize(Transform target, CharacterHealth healthComponent)
    {
        characterHealth = healthComponent;
        HalthbarSlider.maxValue = characterHealth.maxHealth;
        HalthbarSlider.value = characterHealth.currentHealth;
        characterHealth.OnHealthChanged += UpdateHealthBar;
        characterHealth.OnDeath += onCharacterDeath;
    }

    void Update()
    {
        if (characterHealth == null) return;

        transform.position = characterHealth.gameObject.transform.position + offset;
        transform.LookAt(Camera.main.transform);
    }

    private void OnDestroy()
    {
        if(characterHealth == null) return;
        characterHealth.OnHealthChanged -= UpdateHealthBar;
        characterHealth.OnDeath -= onCharacterDeath;
    }


    public void UpdateHealthBar(float health)
    {
        HalthbarSlider.value = health;
    }

    public void onCharacterDeath()
    {
        HalthbarSlider.gameObject.SetActive(false);
    }
}
