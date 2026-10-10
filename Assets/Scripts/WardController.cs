using UnityEngine;

public class WardController : MonoBehaviour
{
    public float wardRadius = 12.5f; 
    public GameObject BasicWard;

    void Start ()
    {
        
    }

    void Update ()
    {
        
    }

    public void ActivateWard ()
    {
        BasicWard.SetActive(true);
        InvokeRepeating("DealDamage", 1f, 0.5f);
            Debug.Log("Activated ward");
    }

    public void DeactivateWard ()
    {
        CancelInvoke("DealDamage");
        BasicWard.SetActive(false);
            Debug.Log("Deactivated ward");
    }

    void DealDamage ()
    {
        Collider[] thingsWithinWard = Physics.OverlapSphere(transform.position, wardRadius);
        foreach(Collider thingInWard in thingsWithinWard)
        {
            EnemyController enemyController = thingInWard.gameObject.GetComponent<EnemyController>();
            BasehunterController hunterController = thingInWard.gameObject.GetComponent<BasehunterController>();
            if(enemyController != null)
            {
                enemyController.TakeDamage(10);
                Debug.Log("Ward damaged Enemy");
            }
            if(hunterController != null)
            {
                hunterController.TakeDamage(10);
                Debug.Log("Ward damaged Basehunter");
            }
        }
    }
}