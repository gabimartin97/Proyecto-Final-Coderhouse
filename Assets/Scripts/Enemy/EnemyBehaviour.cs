using UnityEngine;
using System;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
        
    [SerializeField] protected EnemyData data;
    [SerializeField] Animator enemyAnimator;

    [SerializeField] protected AudioClip[] monsterClips;

    protected AudioSource sound;
    protected enum monsterSounds
    {
        Growl,
        Damage,
        Attack,
        Warp,
        Death,
        count
    }

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
        sound = GetComponent<AudioSource>();
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
                
                damageCooldownTimer = 0f;                
            }
        }
        
        if (health <= 0)
        {
            OnDead?.Invoke(1); //el signo ? es para preguntarse si hay suscriptores al evento, sino da error
            SoundsManager.PlaySound(monsterClips[(int)monsterSounds.Death]);
            Destroy(gameObject);
            
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
           if(!isAttacking) StartCoroutine(Attack(collision.gameObject));
                        
        }
    }

    public virtual void RecieveDamage(float damage)
    {
        health -= damage;
        sound.clip = monsterClips[(int)monsterSounds.Damage];
        sound.pitch = (UnityEngine.Random.Range(0.8f, 1f));
        sound.Play();

    }

    protected IEnumerator Attack(GameObject target)
    {
        if (!damageInCooldown && !isAttacking)
        {
            
            isAttacking = true;
            damageInCooldown = true;
            sound.clip = monsterClips[(int)monsterSounds.Attack];
            sound.pitch = (UnityEngine.Random.Range(0.8f, 1f));
            sound.Play();
            yield return new WaitForSeconds(0.4f);
            target.GetComponent<PlayerBehaviour>().RecieveDamage(damage/2f);
            yield return new WaitForSeconds(0.3f);
            target.GetComponent<PlayerBehaviour>().RecieveDamage(damage / 2f);
            isAttacking = false;
        }
        
    }

    protected void Move()
    {
        if (!GameManager.IsGameOver)
        {
            Vector3 direction = target.transform.position - transform.position;
            if (direction.magnitude <= data.trackDistance)
            {
                if (direction.magnitude >= 0.5f)
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
        if (navMeshAgent.remainingDistance >= 0.5 && !isAttacking)
        {
            enemyAnimator.SetBool("isRunning", true);

        }
        else
        {
            
            enemyAnimator.SetBool("isRunning", false);

        }
    }
}
