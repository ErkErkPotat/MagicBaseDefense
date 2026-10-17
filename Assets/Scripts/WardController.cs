using UnityEngine;
using System.Collections.Generic;

public class WardController : MonoBehaviour
{ 
    public GameObject BasicWard;
    public Transform baseTransform;
    public List<WardstoneController> wardstones = new List<WardstoneController>();
    public LineRenderer placementCircle;

    public float minWardRadius = 8f;
    public float maxWardRadius = 30f;
    public float wardRadius;
    public float minimumStoneDistance = 3f;
    
    public int minimumWardstones = 3;
    public int circleSegments = 100;
    
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
            UpdateWardSize();
            DrawPlacementCircle();


            //Debug.Log($"Ward radius: {wardRadius}");
        }
    }

    void UpdateWardSize()
    {
        float diameter = wardRadius * 2f;

        BasicWard.transform.localScale =
            new Vector3(diameter, diameter, diameter);
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
        placementCircle.gameObject.SetActive(false);
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

    public bool CanPlaceWardstone(Vector3 position)
    {
        Vector3 offset = position - baseTransform.position;
        offset.y = 0f;

        float distance = offset.magnitude;

        if (wardstones.Count == 0)
        {
            if (distance < minWardRadius || distance > maxWardRadius)
                return false;
        }

        foreach (WardstoneController stone in wardstones)
        {
            Vector3 stoneOffset = position - stone.transform.position;
            stoneOffset.y = 0f;

            if (stoneOffset.magnitude < minimumStoneDistance)
                return false;
        }

        return true;
    }

    void DrawPlacementCircle()
    {
        placementCircle.gameObject.SetActive(true);
        placementCircle.positionCount = circleSegments;

        for (int i = 0; i < circleSegments; i++)
        {
            float angle = i * Mathf.PI * 2f / circleSegments;

            float x = Mathf.Cos(angle) * wardRadius;
            float z = Mathf.Sin(angle) * wardRadius;

            Vector3 position = baseTransform.position + new Vector3(x, -1.9f, z);
            // the magic number -1.9f is because the base is set at y=2 and i want the line to appear 0.1 AGL.            

            placementCircle.SetPosition(i, position);
        }
    }
}