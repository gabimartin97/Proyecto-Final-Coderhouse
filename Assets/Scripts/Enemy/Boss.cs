using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Transform playerTarget;
    [SerializeField] GameObject Proyectile;
    [SerializeField][Range(1f, 20f)] float time = 1f;
    [SerializeField][Range(1f, 30f)] float repeatRate = 10f;

    [SerializeField] int bossLife = 3000;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawns", time, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(playerTarget);
        // Calculate the direction
        var direction = playerTarget.position - transform.position;
        direction.y = 0;
        // Make the transform look in the direction.
        transform.forward = direction;
    }
    void Spawns()
    {
        Instantiate(Proyectile, transform.position, transform.rotation);
    }
}
