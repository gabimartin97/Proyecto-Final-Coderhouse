using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float health = 100f;
    [SerializeField] GameObject Weapon;
    [SerializeField] Transform WeaponHand;

    public float Health { get => health; set => health = value; }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            GameManager.IsGameOver = true;
            gameObject.SetActive(false);
        }
    }
    public void RecieveDamage(float damage)
    {
        health -= damage;
        Debug.Log("Vida restante: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            Destroy(Weapon); //Destruye el arma que tiene en la mano. Mejorar
            Weapon = collision.gameObject;
            Weapon.transform.SetParent(WeaponHand);
            Weapon.transform.localPosition = Vector3.zero;
            Weapon.transform.localRotation = Quaternion.identity;
            Weapon.GetComponent<Gun>().enabled = true;
            Weapon.GetComponent<Gun>().PlayerRb = GetComponent<Rigidbody>();

        }
    }
}
