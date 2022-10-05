using UnityEngine;
using System;


public class EnemyBehaviour : MonoBehaviour
{
    
   
    
   [SerializeField] protected EnemyData data;
    protected bool damageInCooldown = false;
    protected float damageCooldownTimer = 0f;

    static public event Action<int> OnDead;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Attack();
        if ( data.life<= 0)
        {
            Destroy(gameObject);
            OnDead.Invoke(1);

        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!damageInCooldown)
            {
                collision.gameObject.GetComponent<PlayerBehaviour>().RecieveDamage(data.damage);
                damageInCooldown = true;
            }

        }
    }

    public virtual void RecieveDamage(float damage)
    {
        data.life -= damage;
    }

    protected void Attack()
    {
        if (damageInCooldown)
        {
            damageCooldownTimer += Time.deltaTime;
            if (damageCooldownTimer >= data.damageCooldown)
            {
                damageInCooldown = false;
                damageCooldownTimer = 0f;
            }
        }
    }
}
