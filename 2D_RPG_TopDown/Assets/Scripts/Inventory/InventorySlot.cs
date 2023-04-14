using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weponInfo;

    public WeaponInfo GetWeaponInfo()
    {
        return weponInfo;
    }
}
