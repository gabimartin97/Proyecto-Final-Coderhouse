using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData data;

    Transform target;
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

        Vector3 distance = transform.position - target.position;
        if (distance.magnitude >= 2f)
        {
            Vector3 targetPosition = target.position;
            transform.LookAt(targetPosition);
            transform.Translate(data.moveSpeed * Time.deltaTime * Vector3.forward);
            //transform.position += Vector3.forward * Time.deltaTime * speed;

        }



    }


    private void FixedUpdate()
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
