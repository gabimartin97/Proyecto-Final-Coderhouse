using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
        
    [SerializeField] protected EnemyData data;
    [SerializeField] Animator enemyAnimator;

    protected float health;
    protected float damage;
    protected float damageCooldown;
    protected float speed;
    protected bool damageInCooldown = false;
    protected float damageCooldownTimer = 0f;
    protected NavMeshAgent navMeshAgent;
    private GameObject target;
    private float wanderTimer = 0f;
    private bool isAttacking = false;
    static public event Action<int> OnDead;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");

        health = data.life;
        damage = data.damage;
        damageCooldown = data.damageCooldown;
        speed = data.speed;
        navMeshAgent.speed = speed;
              
    }

    // Update is called once per frame
    void Update()
    {
            
        Move();
        SetAnimationState();


        if (damageInCooldown)
        {
            damageCooldownTimer += Time.deltaTime;
            if (damageCooldownTimer >= damageCooldown)
            {
                damageInCooldown = false;
                isAttacking = false;
                damageCooldownTimer = 0f;                
            }
        }
        
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
            Attack(collision);
            
        }
    }

    public virtual void RecieveDamage(float damage)
    {
        health -= damage;
        
    }

    protected void Attack(Collision collision)
    {
        if (!damageInCooldown)
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().RecieveDamage(damage);
            isAttacking = true;
            damageInCooldown = true;
        }
    }

    protected void Move()
    {
        if (!GameManager.IsGameOver)
        {
            Vector3 direction = target.transform.position - transform.position;
            if (direction.magnitude <= data.trackDistance)
            {
                if (direction.magnitude >= 2f)
                {
                    navMeshAgent.SetDestination(target.transform.position);
                }
            }
            else
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer >= data.wanderTime)
                {
                    wanderTimer = 0f;
                    Wander(); //Patrullar
                }

            }
        }
    }

    void Wander() //Se mueve a una posicion aleatora para simular patrulla
    {
        navMeshAgent.SetDestination(transform.position + new Vector3(UnityEngine.Random.Range(-4, 4), 0, UnityEngine.Random.Range(-4, 4)));

    }

    void SetAnimationState()
    {
        if(isAttacking)
        {
         enemyAnimator.SetBool("isAttacking", true);
        }
        else
        {
        enemyAnimator.SetBool("isAttacking", false);
        }
        if (navMeshAgent.remainingDistance >= 0.5)
        {
            enemyAnimator.SetBool("isRunning", true);

        }
        else
        {
            
            enemyAnimator.SetBool("isRunning", false);

        }
    }
}
