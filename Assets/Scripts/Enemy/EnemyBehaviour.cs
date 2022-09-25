using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] float damageCooldown = 0.5f;
    // Start is called before the first frame update
    private bool damageInCooldown = false;
    
    private float damageCooldownTimer = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Score++;
            
        }

        if (damageInCooldown)
        {
            damageCooldownTimer += Time.deltaTime;
            if (damageCooldownTimer >= damageCooldown)
            {
                damageInCooldown = false;
                damageCooldownTimer = 0f;
            }
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
     if(collision.gameObject.CompareTag("Player"))
        {
            if(!damageInCooldown)
            {
                collision.gameObject.GetComponent<PlayerBehaviour>().RecieveDamage(damage);
                damageInCooldown = true;
            }
            
        }
    }

    public void RecieveDamage(float damage)
    {
        health -= damage;
    }
}
