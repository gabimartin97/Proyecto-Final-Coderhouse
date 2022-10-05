using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class SpecialEnemy : EnemyBehaviour
{
    new static public event Action<int> OnDead;
    public override void RecieveDamage(float damage)
    {
        health -= damage;
        Warp();
    }
    private void Warp()
    {
        Vector3 newPosition = new Vector3(UnityEngine.Random.Range(-4, 4), 0, UnityEngine.Random.Range(-4, 4));
        gameObject.GetComponent<NavMeshAgent>().Warp(newPosition);
    }

    void Update()
    {

        Attack();
        if (health <= 0)
        {
            Destroy(gameObject);
            OnDead.Invoke(1);

        }


    }
}
