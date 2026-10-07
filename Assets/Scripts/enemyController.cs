using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float maximumHealth = 100f;
    public float currentHealth = 100f;

    public Transform player;

    public float enemySpeed = 1f;
    // keeping these here in case I need them in the future. (will be needing)
    void Start      (){}
    void Update     ()
    {            
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += transform.forward * enemySpeed * Time.deltaTime;
    }

    void TakeDamage (float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("Enemy took " + damage + " damage. Health: " + currentHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        spellController spell = collision.gameObject.GetComponent<spellController>();
        if(spell != null)
        {
            TakeDamage(spell.spellDamage);
        }
    }
}