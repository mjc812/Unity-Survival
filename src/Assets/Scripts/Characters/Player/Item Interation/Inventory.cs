using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject inventoryDisplay;
    private List<Slot> slots = new List<Slot>(25);
    private int inventorySize = 25;
    private bool active;

    private void Awake()
    {
        active = false;
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
        if (active)
        {
            Cursor.lockState = CursorLockMode.Locked;   // should be done in keyinputhandler, and keyinputhandler should check if player is shooting, building, or in UI
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        active = !active;
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
