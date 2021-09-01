using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    private PlayerPickup playerPickup;
    private Inventory inventory;
    private FPWeaponHolderController weaponHolderController;
    private BuildSystem buildSystem;

    void Awake()
    {
        playerPickup = GameObject.FindWithTag("Player").GetComponent<PlayerPickup>();
        buildSystem = GameObject.FindWithTag("Player").GetComponent<BuildSystem>();
        weaponHolderController = GameObject.FindWithTag("FPWeaponHolder").GetComponent<FPWeaponHolderController>();
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        HandleKeyInput();
    }

    void HandleKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            playerPickup.ItemPickup();
        } else if(Input.GetKeyDown(KeyCode.Q))
        {
            weaponHolderController.DropItem();
        } else if(Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None; // make this toggle
            inventory.ToggleInventoryDisplay();
        } else if(Input.GetKeyDown(KeyCode.H))
        {
            if(!buildSystem.IsBuilding())
            {
                buildSystem.BuildFoundation();
            }
        } else if(Input.GetKeyDown(KeyCode.J))
        {
            if(!buildSystem.IsBuilding())
            {
                buildSystem.BuildWall();
            }
        } else if(Input.GetKeyDown(KeyCode.G))
        {
            if(buildSystem.IsBuilding())
            {
                buildSystem.CancelBuild();
            }
        }
    }
}