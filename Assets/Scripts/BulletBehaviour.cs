using UnityEngine;
using UnityEngine.AI;
public class BulletBehaviour : MonoBehaviour
{
    
    [SerializeField] float damage = 25f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 3f);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime * (-1));
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public float GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Transform collisionTransform = collision.transform;

                collision.gameObject.GetComponent<NavMeshAgent>().Warp(collisionTransform.position + collisionTransform.TransformDirection(Vector3.back) * 0.5f);
                collision.gameObject.GetComponent<EnemyBehaviour>().RecieveDamage(damage);
            }


            Destroy();
        }

        
    }
}
