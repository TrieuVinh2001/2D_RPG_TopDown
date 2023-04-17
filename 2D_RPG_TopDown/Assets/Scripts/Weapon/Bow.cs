using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour,IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator anim;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");//Dùng để đỡ tốn tài nguyên, giúp tăng hiệu suất

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        //anim.SetTrigger("Fire");
        anim.SetTrigger(FIRE_HASH);//đỡ tốn tài nguyên hơn
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
