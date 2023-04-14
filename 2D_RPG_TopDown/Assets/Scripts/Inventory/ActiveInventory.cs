using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.KeyBoard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue-1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach(Transform inventorySlot in this.transform)//Tìm trong tất cả các mục con của nó
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);//Ẩn mục con thứ 1 của mục con đó
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);//Hiện mục con thứ của con thứ indexNum 

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name);
    }
}
