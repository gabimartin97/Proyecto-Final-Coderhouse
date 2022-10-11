using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAgent : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private float wanderTimer =0f;
    
    [SerializeField] private EnemyData data;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
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
                if(wanderTimer >= data.wanderTime)
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

}
