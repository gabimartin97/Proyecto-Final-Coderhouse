using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Transform playerTarget;
    [SerializeField] GameObject Proyectile;
    [SerializeField][Range(1f, 20f)] float time = 5f;
    [SerializeField][Range(1f, 30f)] float repeatRate = 5f;

    [SerializeField] int bossLife = 3000;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawns", time, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTarget);
    }
    void Spawns()
    {
        Instantiate(Proyectile, transform.position + Vector3.up * 5 + Vector3.forward * 5, transform.rotation);
    }
    void LastForm()
    {
        if (bossLife <= 1000)
        {
            repeatRate = 2f;
        }
    }
}
