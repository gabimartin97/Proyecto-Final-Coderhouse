using UnityEngine;

public class EnemyMovement_Force : MonoBehaviour
{
    [SerializeField] EnemyType enemyType;
    [SerializeField] Transform shootPoint;
    [SerializeField][Range(2f, 20f)] float rayDistance = 50f;
    [SerializeField] float moveForce = 1000f;
    [SerializeField] float rotateForce = 500f;

    enum EnemyType
    {
        Berserker,
        Sniper
    }

    Rigidbody myRigidbody;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void FixedUpdate()
    {
        LookRayCast();
        LookAndMove();

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

    void LookAndMove()
    {
        switch (enemyType)
        {
            case EnemyType.Berserker:
                Vector3 direction = target.position - transform.position;
                direction.y = 0f;
                if (direction.magnitude >= 2.5f)
                {
                    Quaternion q = Quaternion.LookRotation(direction);
                    myRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, q, rotateForce * Time.deltaTime));
                    if (myRigidbody.velocity.magnitude <= 5f) myRigidbody.AddForce(direction.normalized * moveForce, ForceMode.Force);

                }

                break;
            case EnemyType.Sniper:

                break;

        }
    }
}
