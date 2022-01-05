using UnityEngine;

public class Weapon : MonoBehaviour, Item
{
    private FPWeaponHolderController weaponHolderController;
    private Animator animator;

    public int ID {
        get => 0;
    }

    public string Description {
        get => "Test Weapon";
    }

    public Sprite Sprite
    {
        get => null;
    }

    public void PickUp()
    {
        weaponHolderController = GameObject.FindWithTag("FPWeaponHolder").GetComponent<FPWeaponHolderController>();
        weaponHolderController.HoldItem(this);
    }

    public bool Use()
    {
        Debug.Log(Description + "used");
        return true;
    }
}
