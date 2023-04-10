using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashAnimationPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    private PlayerControls playerControls;
    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown = false;
    private bool isAttacking = false;

    private GameObject slashAnim; 

    private void Awake()
    {
        playerController = GetComponentInParent <PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        anim = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponCollider.gameObject.SetActive(false);
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            slashAnim = Instantiate(slashPrefab, slashAnimationPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
        
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    public void DoneAttack()//Dung trong animation Sword
    {
        weaponCollider.gameObject.SetActive(false);//Ẩn collider của weapon
    }

    public void SwingUpFlipAnim()//Dùng trong Animation Slash
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);//Đổi chiều xoay

        if (playerController.FacingLeft)//Kiểm tra hướng
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;//đổi hướng
        }
    }

    public void SwingDownFlipAnim()//Dùng trong Animation Slash
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);//Đổi chiều xoay

        if (playerController.FacingLeft)//Kiểm tra hướng
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;//đổi hướng
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;//Chiều xoay của vũ khí

        if (mousePos.x < playerScreenPoint.x)//Hướng vũ khí theo 1 phía của chuột
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, -180f, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
