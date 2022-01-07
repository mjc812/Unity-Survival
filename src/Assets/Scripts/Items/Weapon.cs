using UnityEngine;

public class Weapon : MonoBehaviour, Item
{
    private FPWeaponHolderController weaponHolderController;
    private ParticleSystem flashParticleSystem;
    private AudioSource audioSource;

    //eventually move to object pool. Do not want to reference prefabs in random places everywhere, should only be done through object pool handlers
    public GameObject spark;

    public AudioClip fire;

    private float fireRate = 13f;
    private float nextTimeToFire;

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

    void Start()
    {
        nextTimeToFire = 0;
        audioSource = transform.GetComponent<AudioSource>();
        Transform muzzleFlash = gameObject.transform.Find("MuzzleFlash");
        Transform flare = muzzleFlash.Find("Flash");
        flashParticleSystem = flare.GetComponent<ParticleSystem>();
    }

    public void PickUp()
    {
        weaponHolderController = GameObject.FindWithTag("FPWeaponHolder").GetComponent<FPWeaponHolderController>();
        weaponHolderController.HoldItem(this);
    }

    public bool Use()
    {
        if (Time.time > nextTimeToFire)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 80f))
            {
                GameObject spark2 = Instantiate(spark, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(spark2, 0.5f);
            }
            audioSource.PlayOneShot(fire, 1f);
            nextTimeToFire = Time.time + 1f / fireRate;
            flashParticleSystem.Play();
            return true;
        } else
        {
            return false;
        }
    }
}
