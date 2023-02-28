using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private float currentHealth;
    [SerializeField] private Text text;
    [SerializeField] private Image hpBar;

    private void Start() {
        currentHealth = health;
    }

    private void Update() {
        hpBar.fillAmount =  currentHealth / health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
        if (enemyHealth)
        {
            if (currentHealth > 0 && !enemyHealth.isDead)
            {
                currentHealth -= 10;
            }
            else {
                SceneManager.LoadScene("WinLose");
                int loadedNumber = PlayerPrefs.GetInt("Win");
                PlayerPrefs.SetInt("Lose", loadedNumber + 1);
                PlayerPrefs.Save();
            }
            Debug.Log(currentHealth);
        }
    }
}
