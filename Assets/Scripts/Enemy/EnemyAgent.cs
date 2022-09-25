using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAgent : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(! GameManager.IsGameOver)
        {
            Vector3 direction = target.transform.position - transform.position;
            if (direction.magnitude >= 2f)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
        }
        
        
    }
}
