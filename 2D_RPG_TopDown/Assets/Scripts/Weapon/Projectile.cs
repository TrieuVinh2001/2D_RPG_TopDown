using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleVFX;
    private Vector3 startPosition;

    private WeaponInfo weaponInfo;

    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        InDestructible inDestructible = collision.gameObject.GetComponent<InDestructible>();
        if(!collision.isTrigger && (inDestructible||enemyHealth))
        {
            //if (enemyHealth)
            //{
            //    enemyHealth.TakeDamage(weaponInfo.weaponDame);
            //}
            Instantiate(particleVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
