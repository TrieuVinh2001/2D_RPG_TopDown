using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat=2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private State state;
    private EnemyPathFinding enemyPathFinding;

    private Vector2 roamingPosition;
    private float timeRoaming=2f;
    

    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }
    // Start is called before the first frame update
    void Start()
    {
        roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;

        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathFinding.MoveTo(roamingPosition);

        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamingPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (canAttack)
        {
            canAttack = false;
            //enemyType là script loại tấn công sẽ gán vào để thực hiện hàm Attack của script đó
            //ví dụ gán Shooter thì thực hiện Attack() trong Script Shooter
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                enemyPathFinding.StopMoving();
            }
            else
            {
                enemyPathFinding.MoveTo(roamingPosition);
            }

            StartCoroutine(AttackCoolDownRoutine());
        }
        
    }

    private IEnumerator AttackCoolDownRoutine()
    { 
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    //private IEnumerator RoamingRouting()
    //{
    //    while(state == State.Roaming)
    //    {
    //        Vector2 roamPosition = GetRoamingPosition();
    //        enemyPathFinding.MoveTo(roamPosition);//di chuyển đến vị trí ngẫu nhiên đã chọn
    //        yield return new WaitForSeconds(2f);

    //    }
    //}

    private Vector2 GetRoamingPosition()//Lấy vị trí ngẫu nhiên
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
