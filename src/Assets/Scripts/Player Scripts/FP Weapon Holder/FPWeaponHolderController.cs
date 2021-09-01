using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPWeaponHolderController : MonoBehaviour
{
    private Weapon item = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        if (item)
        {
            item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
            item = null;
        }
    }

    public void HoldItem(Weapon weapon)
    {
        DropItem();
        item = weapon;
        Transform itemTransform = weapon.transform;
        itemTransform.parent = transform;
        itemTransform.localPosition = new Vector3(0.5f, -0.35f, 0.7f);
        itemTransform.localRotation = Quaternion.Euler(0f, -175f, 0f);
        itemTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void UseItem()
    {
        if (item)
        {
            item.Use();
        }
    }
}
