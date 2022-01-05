using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPWeaponHolderController : MonoBehaviour
{
    private Weapon item = null;
    private Animator animator;

    //make private later
    public float positionX = 0.2f;
    public float positionY = -0.35f;
    public float positionZ = 0.9f;

    public float rotationX = 0f;
    public float rotationY = -182f;
    public float rotationZ = 0f;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

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
        itemTransform.localPosition = new Vector3(positionX, positionY, positionZ);
        itemTransform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        itemTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void NotUsingItem()
    {
        //animator.SetBool("Fire", false);
    }

    public void UseItem()
    {
        if (item)
        {
            bool used = item.Use();
            if (used)
            {
                animator.SetTrigger("TestFire");
                //animator.SetBool("Fire", true);
            }
        }
    }
}
