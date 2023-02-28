using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private float currentHealth;

    private EnemyAI enemyAI;
    [HideInInspector] public bool isDead;
    public Image hpBar;

    private void Start()
    {
        currentHealth = health;
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            hpBar.fillAmount =  currentHealth / health;
            if (currentHealth <= 0)
            {
                Death();
            }
            else
            {
                Debug.Log("Hit");
            }
        }
    }

    private void Death()
    {
        enemyAI.isDead = true;
        enemyAI.anim.SetTrigger("Dying");
        Debug.Log("Death");
    }
}