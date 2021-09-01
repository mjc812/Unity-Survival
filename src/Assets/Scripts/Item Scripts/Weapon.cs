using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, Item
{
    private FPWeaponHolderController weaponHolderController;
    protected int IDNumber;
    protected string DescriptionString;
    public int ID
    {
        get => IDNumber;
    }

    public string Description
    {
        get => DescriptionString;
    }

    public void PickUp()
    {
        weaponHolderController = GameObject.FindWithTag("FPWeaponHolder").GetComponent<FPWeaponHolderController>();
        weaponHolderController.HoldItem(this);
    }

    public void Use()
    {
        Debug.Log("weapon used");
    }
}
