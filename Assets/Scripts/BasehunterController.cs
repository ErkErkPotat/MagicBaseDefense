using  UnityEngine;

public class BasehunterController : MonoBehaviour
{
    // For now using the location of the base as the goal and will have the enemies just travel straight towards the location of the empty
    // In the future when i get more complex bases (Base with wall, castle, etc.) will work with specifically designated target points.
    // Such as gates, any point of a weak wall, gatehouses etc.
    public float maximumHealth = 100f;
    public float currentHealth = 100f;
    public float enemySpeed = 1f;
    public bool isAttacking = false;

    public Transform target;

    void Update     ()
    {            
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        
        if(direction != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(direction);
            if(distanceToTarget >= 5){
                //hardcoded distance due to geometry in prototype version of game.
                //commented out for now but if the enemy ever starts moving away from attack range
                //isAttacking = false;
                transform.position += transform.forward * enemySpeed * Time.deltaTime;
            } else if (isAttacking == false) {
                isAttacking = true;
                InvokeRepeating("AttackBase", 1f, 1f);
            }
        }
    }

    public void TakeDamage (float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            isAttacking = false;
            CancelInvoke("AttackBase");
            Destroy(gameObject);
        }
    }

    public void AttackBase ()
    {
        BaseController baseController = target.gameObject.GetComponent<BaseController>();
        baseController.TakeDamage(50);
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