using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum EnemyType
    {
        Berserker,
        Sniper
    }

    [SerializeField] EnemyType enemyType;
    Transform target;
    [SerializeField] float speed = 5f;
    [SerializeField] Transform shootPoint;
    [SerializeField][Range(2f, 20f)] float rayDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Berserker:
                Vector3 distance = transform.position - target.position;
                if (distance.magnitude >= 2f)
                {
                    Vector3 targetPosition = target.position;
                    transform.LookAt(targetPosition);
                    transform.Translate(speed * Time.deltaTime * Vector3.forward);
                    //transform.position += Vector3.forward * Time.deltaTime * speed;

                }

                break;
            case EnemyType.Sniper:
                Quaternion newRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 10f);
                break;

        }

    }
    void FixedUpdate()
    {
        LookRayCast();

    }
    void LookRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Mirando a Player");
            }
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = shootPoint.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(shootPoint.position, direction);
    }
}
