using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class SpecialEnemy : EnemyBehaviour
{
    //new static public event Action<int> OnDead;
    public override void RecieveDamage(float damage)
    {
        health -= damage;
       
        Warp();                 //Se teletransporta cuando recibe daño
    }

    private void Warp() 
    {
        Vector3 newPosition = transform.position -  new Vector3(UnityEngine.Random.Range(-4, 4), 0, UnityEngine.Random.Range(-4, 4));
        navMeshAgent.Warp(newPosition);
        sound.clip = monsterClips[(int)monsterSounds.Warp];
        sound.pitch = (UnityEngine.Random.Range(0.8f, 1f));
        sound.Play();
    }
       
}
