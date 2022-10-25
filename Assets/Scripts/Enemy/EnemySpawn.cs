using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject Spawned;
    [SerializeField][Range(1f, 20f)] float time = 1f;
    [SerializeField][Range(1f, 30f)] float repeatRate = 10f;
    bool invoking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !invoking)
        {
            InvokeRepeating("Spawns", time, repeatRate);
            invoking = true;
        }
    }
    void Spawns()
    {
        Instantiate(Spawned, transform.position, transform.rotation);
    }
}
