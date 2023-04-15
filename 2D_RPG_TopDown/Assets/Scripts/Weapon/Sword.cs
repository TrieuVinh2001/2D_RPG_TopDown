using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashPrefab;
    private Transform slashAnimationPoint;
    private Transform weaponCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    private Animator anim;

    private GameObject slashAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimationPoint = GameObject.Find("SlashAnimationPoint").transform;
        weaponCollider.gameObject.SetActive(false);
        
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        //isAttacking = true;
        anim.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashPrefab, slashAnimationPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    public void DoneAttack()//Dung trong animation Sword
    {
        weaponCollider.gameObject.SetActive(false);//Ẩn collider của weapon
    }

    public void SwingUpFlipAnim()//Dùng trong Animation Slash
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);//Đổi chiều xoay

        if (PlayerController.Instance.FacingLeft)//Kiểm tra hướng
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;//đổi hướng
        }
    }

    public void SwingDownFlipAnim()//Dùng trong Animation Slash
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);//Đổi chiều xoay

        if (PlayerController.Instance.FacingLeft)//Kiểm tra hướng
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;//đổi hướng
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;//Chiều xoay của vũ khí

        if (mousePos.x < playerScreenPoint.x)//Hướng vũ khí theo 1 phía của chuột
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, -180f, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
