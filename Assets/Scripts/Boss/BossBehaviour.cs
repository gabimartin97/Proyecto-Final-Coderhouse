using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] Transform playerTarget;
    [SerializeField] GameObject Proyectile;
    [SerializeField][Range(1f, 20f)] float time = 5f;
    [SerializeField][Range(1f, 30f)] float repeatRate = 5f;

    [SerializeField] float health = 3000f;
    [SerializeField] protected AudioClip[] bossClips;
    private AudioSource sound;
    private int bossFase = 0;
    bool isSpawningFirballs = false;
    Transform attackSource;

    //EVENTOS
    static public event Action OnDead;
    //EVENTOS
    protected enum bossSounds
    {
        Growl,
        Damage,
        Attack,
        Warp,
        Death,
        count
    }

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        attackSource = transform.Find("AttackSource");
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf && !isSpawningFirballs)
        {
            isSpawningFirballs = true;
            InvokeRepeating("Spawns", time, repeatRate);

        }
        LookAtPlayer();

        if (health <= 1000 && bossFase < 1)
        {
            bossFase++;
            LastForm();
        }
            
            

        if (health <= 0)
        {
            OnDead?.Invoke(); //el signo ? es para preguntarse si hay suscriptores al evento, sino da error
            SoundsManager.PlaySound(bossClips[(int)bossSounds.Death]);
            Destroy(gameObject);

        }
    }
    void Spawns()
    {
        Instantiate(Proyectile, attackSource.position, transform.rotation);
        sound.clip = bossClips[(int)bossSounds.Attack];
        sound.pitch = (UnityEngine.Random.Range(0.8f, 1f));
        sound.Play();
    }
    void LastForm()
    {
        CancelInvoke("Spawns");
        isSpawningFirballs = false;
         repeatRate = 2f;
        
    }

    public void RecieveDamage(float damage)
    {
        health -= damage;
        sound.clip = bossClips[(int)bossSounds.Damage];
        sound.pitch = (UnityEngine.Random.Range(0.8f, 1f));
        sound.Play();
        Debug.Log("VIDA BOSS= " + health);
    }

    private void LookAtPlayer()
    {
        // transform.LookAt(playerTarget);
        // Calculate the direction
        var direction = playerTarget.position - transform.position;
        direction.y = 0;
        // Make the transform look in the direction.
        transform.forward = direction;

    }
}
