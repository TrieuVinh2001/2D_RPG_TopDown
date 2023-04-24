using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleVFX;
    private Vector3 startPosition;

    [SerializeField] private bool isEnemyProjectile;//Script có của enemy không
    [SerializeField] private float projectileRange = 10f;

    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        InDestructible inDestructible = collision.gameObject.GetComponent<InDestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (!collision.isTrigger && (inDestructible || enemyHealth||player))
        {
            if ((player && isEnemyProjectile)||(enemyHealth && !isEnemyProjectile))
            {
                //if (player) { player.TakeDamage(1, transform); }
                player?.TakeDamage(1, transform);
                Instantiate(particleVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if(!collision.isTrigger && inDestructible)//Nếu k tích IsTrigger và có script InDestructible
            {
                Instantiate(particleVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
