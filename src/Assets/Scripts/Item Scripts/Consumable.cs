using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour, Item
{
    private Inventory inventory;
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
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        if (inventory.AddItem(this))
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void Use()
    {
        Debug.Log("consumable used");
    }
}
