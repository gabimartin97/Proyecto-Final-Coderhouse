using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossProyectile : MonoBehaviour
{
    
    [SerializeField] float damage = 25f;
    [SerializeField] float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 5f);

    }

    // Update is called once per frameprivate
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Boss") )
        {
            return;
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Transform collisionTransform = collision.transform;

                collision.gameObject.GetComponent<NavMeshAgent>().Warp(collisionTransform.position + collisionTransform.TransformDirection(Vector3.back)* 0.5f);
                collision.gameObject.GetComponent<EnemyBehaviour>().RecieveDamage(damage);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerBehaviour>().RecieveDamage(damage);
            }

                Destroy();
        }

        
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public float GetDamage()
    {
        return damage;
    }
}
