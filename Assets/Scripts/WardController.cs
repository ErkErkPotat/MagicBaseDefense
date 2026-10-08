using UnityEngine;

public class WardController : MonoBehaviour
{
    public float wardRadius = 12.5f; 
    void Start ()
    {
        InvokeRepeating("DealDamage", 1f, 0.5f);
    }

    void Update ()
    {
        
    }

    void DealDamage ()
    {
        Collider[] thingsWithinWard = Physics.OverlapSphere(transform.position, wardRadius);
        foreach(Collider thingInWard in thingsWithinWard)
        {
            EnemyController enemyController = thingInWard.gameObject.GetComponent<EnemyController>();
            if(enemyController != null)
            {
                enemyController.TakeDamage(10);
                Debug.Log("Ward Damaged Enemy");
            }
        }
    }
}