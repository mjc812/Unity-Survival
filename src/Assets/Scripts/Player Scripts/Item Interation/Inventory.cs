using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject inventoryDisplay;
    private List<Slot> slots = new List<Slot>(25);
    private int inventorySize = 25;

    private void Awake()
    {
        inventoryDisplay = GameObject.FindWithTag("InventoryDisplay");
        GameObject[] slotGameObjects = GameObject.FindGameObjectsWithTag("Slot");
        foreach (GameObject slotGameObject in slotGameObjects)
        {
            slots.Add(slotGameObject.GetComponent<Slot>());
        }
    }

    private void Start()
    {
        inventoryDisplay.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            /*for (int i = 0; i < inventorySize; i++)
            {
                Slot slot = slots[i];
            }*/
        }
    }

    public void ToggleInventoryDisplay()
    {
        inventoryDisplay.SetActive(!inventoryDisplay.activeInHierarchy);
    }

    public bool AddItem(Consumable consumable)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            Slot slot = slots[i];
            if (slot.AddItem(consumable))
            {
                return true;
            }
        }
        return false;
    }
}
