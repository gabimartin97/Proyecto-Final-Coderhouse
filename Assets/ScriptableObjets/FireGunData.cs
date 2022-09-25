using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FireGunData",menuName ="FireGunData")]
public class FireGunData : ScriptableObject
{
    [Header("POWER")]

    [SerializeField]
    [Range(1f, 200f)]
    public float shootForce = 40f;

    [SerializeField]
    public float recoilForce = 40f;

    [SerializeField]
    public float damage = 25f;

    [Header("SPEED")]

    [SerializeField]
    [Tooltip("Time delay until you are allowed to fire again")]
    public float timeBetweenShooting = 0.1f;

    [SerializeField]
    public float reloadTime = 2f;

    [SerializeField]
    [Tooltip("Time delay for succesive bullets")]
    public float timeBetweenShots = 0.1f;

    [Header("OTHERS")]

    [SerializeField]
    [Range(0f,1f)]
    public float spread = 0.1f;

    [SerializeField]
    public int magazineSize = 24;

    [SerializeField]
    [Tooltip("How many bullets are fired with one click")]
    public int bulletsPerTap = 1;

    [SerializeField]
    public bool allowButtonHold = true;
    

}
