using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [HideInInspector] public WeaponManger weapon;
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
        EnemyAI enemyAI = collision.gameObject.GetComponentInParent<EnemyAI>();
        if (enemyAI)
        {
            enemyHealth.TakeDamage(weapon.damage);
            if (enemyHealth.health <= 0 && !enemyHealth.isDead)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(direction * weapon.enemyKickbackForce, ForceMode.Impulse);
                enemyHealth.isDead = true;
            }
        }

        Destroy(gameObject);
    }
}