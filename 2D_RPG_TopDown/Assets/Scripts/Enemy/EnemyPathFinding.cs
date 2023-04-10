using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 moveDir;

    private KnockBack knockBack;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockBack.gettingKnockBack) { return; }//Nếu đang bị bật lại thì k di chuyển được(tránh lỗi k bị bật lại)
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);//Di chuyển
    }

    public void MoveTo(Vector2 TargetPosition)
    {
        moveDir = TargetPosition;//Hướng di chuyển
    }
}
