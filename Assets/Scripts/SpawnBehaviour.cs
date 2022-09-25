using UnityEngine;

public class SpawnBehaviour : MonoBehaviour

{
    [SerializeField] GameObject Spawned;
    [SerializeField][Range(1f, 20f)] float time = 1f;
    [SerializeField][Range(1f, 30f)] float repeatRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawns", time, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Spawns()
    {
        Instantiate(Spawned, transform.position, transform.rotation);
    }
}

