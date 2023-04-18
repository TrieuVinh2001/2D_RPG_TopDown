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

        ToggleActiveHighlight(0);//Khi bắt đầu sẽ cầm vũ khí đầu tiên
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
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)//Nếu đang có vũ khí
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);//Hủy vũ khí đó
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);//Lấy vị trí tương ứng với SlotIndex
        InventorySlot inventoryslot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventoryslot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        //Nếu nhấn slot không có vũ khí(tức ô rỗng) thì return thoát khỏi chức năng này
        if (weaponInfo ==null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        //Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name);
        //GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;//Lấy ra Prefab vũ khí


        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);//Tạo vũ khí ở vị trí ActiveWeapon
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;//Tạo vũ khí bên trong ActiveWeapon

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
