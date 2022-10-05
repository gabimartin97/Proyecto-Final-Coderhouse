using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
        
    [SerializeField] protected EnemyData data;

    protected float health;
    protected float damage;
    protected float damageCooldown;
    protected float speed;
    protected bool damageInCooldown = false;
    protected float damageCooldownTimer = 0f;

    protected NavMeshAgent agent;
    static public event Action<int> OnDead;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        health = data.life;
        damage = data.damage;
        damageCooldown = data.damageCooldown;
        speed = data.speed;
        agent.speed = speed;

      
    }

    // Update is called once per frame
    void Update()
    {

        Attack();
        if (health <= 0)
        {
            OnDead?.Invoke(1); //el signo ? es para preguntarse si hay suscriptores al evento, sino da error
            Destroy(gameObject);
            
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!damageInCooldown)
            {
                collision.gameObject.GetComponent<PlayerBehaviour>().RecieveDamage(damage);
                damageInCooldown = true;
            }

        }
    }

    public virtual void RecieveDamage(float damage)
    {
        health -= damage;
        
    }

    protected void Attack()
    {
        if (damageInCooldown)
        {
            damageCooldownTimer += Time.deltaTime;
            if (damageCooldownTimer >= damageCooldown)
            {
                damageInCooldown = false;
                damageCooldownTimer = 0f;
            }
        }
    }
}
