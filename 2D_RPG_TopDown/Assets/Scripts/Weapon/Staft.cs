using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staft : MonoBehaviour,IWeapon
{
    public void Attack()
    {
        Debug.Log("staft");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
