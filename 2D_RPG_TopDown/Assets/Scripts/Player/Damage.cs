using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damageAmount;//Sát thương

    private void Start()
    {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDame;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            enemyHealth.TakeDamage(damageAmount);//Quái nhận sát thương
        }
    }
}
