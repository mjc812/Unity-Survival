using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    private FPWeaponHolderController weaponHolder;
    private BuildSystem buildSystem;
    private AimSystem aimSystem;

    void Awake()
    {
        weaponHolder = GameObject.FindWithTag("FPWeaponHolder").GetComponent<FPWeaponHolderController>();
        buildSystem = GameObject.FindWithTag("Player").GetComponent<BuildSystem>();
        aimSystem = GameObject.FindWithTag("FPCamera").GetComponent<AimSystem>();
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (buildSystem.IsBuilding())
        //    {
        //        buildSystem.DoBuild();
        //    } else
        //    {
        //        weaponHolder.UseItem();
        //    }
        //}
        if (Input.GetMouseButton(0))
        {
            weaponHolder.UseItem();
        }
        if (Input.GetMouseButtonDown(1))
        {
            aimSystem.AimIn();
        }
        if (Input.GetMouseButtonUp(1))
        {
            aimSystem.AimOut();
        }
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            buildSystem.UpdateSnapThresholdAccumulators(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}