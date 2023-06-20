using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int healthMax = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageTime=1f;//Thời gian không bị nhận dame

    private int currentHealth;
    private bool canTakeDamage = true;

    private KnockBack knockback;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();
        knockback = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();

    }

    private void Start()
    {
        currentHealth = healthMax;
    }

    private void OnCollisionStay2D(Collision2D collision)//Không cần tích trigger nào cả
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, collision.transform);
            
        }
    }

    public void HealPlayer()
    {
        currentHealth += 1;
    }

    public void TakeDamage(int damageAmount, Transform hitTranform)
    {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTranform.gameObject.transform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageTime);
        canTakeDamage = true;
    }
}
