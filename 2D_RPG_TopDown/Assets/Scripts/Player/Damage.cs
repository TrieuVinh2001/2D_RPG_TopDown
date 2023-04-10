using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;//Sát thương

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            enemyHealth.TakeDamage(damageAmount);//Quái nhận sát thương
        }
    }
}
