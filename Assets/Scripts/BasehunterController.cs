using  UnityEngine;

public class BasehunterController : MonoBehaviour
{
    // For now using the location of the base as the goal and will have the enemies just travel straight towards the location of the empty
    // In the future when i get more complex bases (Base with wall, castle, etc.) will work with specifically designated target points.
    // Such as gates, any point of a weak wall, gatehouses etc.
    public float maximumHealth = 100f;
    public float currentHealth = 100f;

    public Transform target;

    public float enemySpeed = 1f;

    void Update     ()
    {            
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if(direction != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }
    }

    public void TakeDamage (float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        //Debug.Log("Enemy took " + damage + " damage. Health: " + currentHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        SpellController spell = collision.gameObject.GetComponent<SpellController>();
        if(spell != null)
        {
            TakeDamage(spell.spellDamage);
        }
    }
}