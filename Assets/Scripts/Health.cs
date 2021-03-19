using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Tooltip("Maximum amount of health")]
    public float maxHealth = 50f;
    [Tooltip("Maximum amount of shield")]
    public float maxShield = 200f;

    public float currentHealth { get; set; }
    public float currentShield { get; set; }
    public bool invincible { get; set; }

    private bool isShielBroke;

    bool m_IsDead;
    private void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (invincible || m_IsDead)
            return;

        // Determine Reduce health or shield
        if(currentShield > 0)
        {
            currentShield -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxShield);
        }
        else
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }
        // Invoke hit damage event
        if(currentHealth <= 0)
            EventManager.current.DamageEnemyEvent.Invoke(true);
        else
            EventManager.current.DamageEnemyEvent.Invoke(false);

        HandleShield();
        HandleDeath();
    }

    private void HandleShield()
    {
        if (!isShielBroke && currentShield <= 0)
        {
            Debug.Log("Shiel broke!");
            isShielBroke = true;
        }
    }

    public void Kill()
    {
        currentHealth = 0f;
        HandleDeath();
    }

    private void HandleDeath()
    {
        if (m_IsDead)
            return;

        if (currentHealth <= 0f)
        {
            m_IsDead = true;
            Destroy(gameObject);
        }
    }

}
