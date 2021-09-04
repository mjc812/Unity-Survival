using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    private FPWeaponHolderController weaponHolder;
    private BuildSystem buildSystem;

    void Awake()
    {
        weaponHolder = GameObject.FindWithTag("FPWeaponHolder").GetComponent<FPWeaponHolderController>();
        buildSystem = GameObject.FindWithTag("Player").GetComponent<BuildSystem>();
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (buildSystem.IsBuilding())
            {
                buildSystem.DoBuild();
            } else
            {
                weaponHolder.UseItem();
            }
        }
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            buildSystem.UpdateSnapThresholdAccumulators(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}