using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;

    private int currenHealth;

    private void Start()
    {
        currenHealth = startHealth;
    }

    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
        Debug.Log(currenHealth);
        Death();
    }

    private void Death()
    {
        if (currenHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
