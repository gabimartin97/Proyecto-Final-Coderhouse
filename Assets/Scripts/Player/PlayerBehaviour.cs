using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField] GameObject weaponInHand;
    [SerializeField] Transform weaponHand;
    [SerializeField] List<GameObject> weaponList;

    private float health = 100f;
    private float maxHealth = 100f;
    static public event Action OnDead;
    static public event Action<float, float> OnHealthChange;
    static public event Action<int> OnWeaponPickedUp;  // 1 -> pistol 2-> yellowRifle 3-> shotgun
    static public event Action<int> OnWeaponInHand; // 1 -> pistol 2-> yellowRifle 3-> shotgun
    public float Health { get => health; set => health = value; }

    // Start is called before the first frame update
    void Start()
    {
        weaponList = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            OnDead?.Invoke();
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           if(weaponList.Count > 0) WeaponToHand(weaponList[0]);


        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (weaponList.Count > 1) WeaponToHand(weaponList[1]);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            if (weaponList.Count > 2) WeaponToHand(weaponList[2]);

        }


    }
    public void RecieveDamage(float damage)
    {
        health -= damage;
        OnHealthChange?.Invoke(health, maxHealth);
        Debug.Log("Vida restante: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
           GameObject pickedUpWeapon = collision.gameObject;
            pickedUpWeapon.SetActive(false);
            weaponList.Add(pickedUpWeapon);

            //HUD
            if (pickedUpWeapon.name == "Pistol") OnWeaponPickedUp?.Invoke(1);
            if (pickedUpWeapon.name == "YellowRifle") OnWeaponPickedUp?.Invoke(2);
            if (pickedUpWeapon.name == "Shotgun") OnWeaponPickedUp?.Invoke(3);
        }
    }

    private void WeaponToHand(GameObject weapon)
    {
        DisableAllWeapon();
        weapon.SetActive(true);
        weapon.transform.SetParent(weaponHand);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.GetComponent<Gun>().enabled = true;
        weapon.GetComponent<Gun>().PlayerRb = GetComponent<Rigidbody>();
        weaponInHand = weapon;

        //HUD
        if (weaponInHand.name == "Pistol") OnWeaponInHand?.Invoke(1);
        if (weaponInHand.name == "YellowRifle") OnWeaponInHand?.Invoke(2);
        if (weaponInHand.name == "Shotgun") OnWeaponInHand?.Invoke(3);

    }
    private void DisableAllWeapon()
    {
        foreach (GameObject Weapon in weaponList)
        {
            Weapon.SetActive(false);
        }
    }
}
