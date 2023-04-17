using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour,IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator anim;
    readonly int AttackHash = Animator.StringToHash("Attack");//Dùng để đỡ tốn tài nguyên, giúp tăng hiệu suất

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }
    public void Attack()
    {
        anim.SetTrigger(AttackHash);//đỡ tốn tài nguyên hơn
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;//Chiều xoay của vũ khí

        if (mousePos.x < playerScreenPoint.x)//Hướng vũ khí theo 1 phía của chuột
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, -180f, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
