using UnityEngine;
using System.Collections.Generic;

public class WardController : MonoBehaviour
{ 
    public GameObject BasicWard;
    public Transform baseTransform;
    public List<WardstoneController> wardstones = new List<WardstoneController>();

    public float wardRadius;
    public int minimumWardstones = 3;
    public bool isActive = false;

    public void RegisterWardstone(WardstoneController stone)
    {
        wardstones.Add(stone);

        stone.parentWard = this;

        if (wardstones.Count == 1)
        {
            Vector3 offset = stone.transform.position - baseTransform.position;
            offset.y = 0f;

            wardRadius = offset.magnitude;
            //Debug.Log($"Ward radius: {wardRadius}");
        }
    }

    public void ActivateWard ()
    {
        if (wardstones.Count < minimumWardstones || isActive)
        {
            return;
        }
        BasicWard.SetActive(true);
        InvokeRepeating("DealDamage", 1f, 0.5f);
        isActive = true;
            //Debug.Log("Activated ward");
    }

    public void DeactivateWard ()
    {
        CancelInvoke("DealDamage");
        BasicWard.SetActive(false);
        isActive = false;
            //Debug.Log("Deactivated ward");
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
                //Debug.Log("Ward damaged Enemy");
            }
            if(hunterController != null)
            {
                hunterController.TakeDamage(10);
                //Debug.Log("Ward damaged Basehunter");
            }
        }
    }
}