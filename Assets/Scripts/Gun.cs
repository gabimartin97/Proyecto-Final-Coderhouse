using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] protected FireGunData data;
    //bullet 
    [SerializeField] GameObject bullet;
    //Graphics
    [SerializeField] GameObject muzzleFlash;

    int bulletsLeft, bulletsShot;
    private Transform attackPoint;
    private Rigidbody playerRb;
    private TextMeshProUGUI ammunitionDisplay;
    //bools
    bool shooting, readyToShoot, reloading;
    //bug fixing :D
    bool allowInvoke = true;

    public Rigidbody PlayerRb { get => playerRb; set => playerRb = value; }

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = data.magazineSize;
        readyToShoot = true;
    }
    private void Start()
    {
        ammunitionDisplay = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        attackPoint = transform.Find("Nozzle");
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        //Set ammo display, if it exists :D
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / data.bulletsPerTap + " / " + data.magazineSize / data.bulletsPerTap);
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (data.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < data.magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;



        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = attackPoint.TransformDirection(Vector3.forward);

        //Calculate spread
        float x = Random.Range(-data.spread, data.spread);
        float y = Random.Range(-data.spread, data.spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction
        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * data.shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
           Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", data.timeBetweenShooting);
            allowInvoke = false;
            //Add recoil to player (should only be called once)
            PlayerRb.AddForce(-directionWithSpread.normalized * data.recoilForce, ForceMode.Impulse);
        }
        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < data.bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", data.timeBetweenShots);

    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", data.reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = data.magazineSize;
        reloading = false;
    }
}
