using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currenHealth;

    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currenHealth = startHealth;
    }

    public void TakeDamage(int damage)
    {
        currenHealth -= damage;
        knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);//Bật lại
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDeath());
    }

    private IEnumerator CheckDeath()
    {
        yield return new WaitForSeconds(flash.GetRestoreMaterialTime());
        Death();
    }

    private void Death()
    {
        if (currenHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
