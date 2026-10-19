using UnityEngine;

public class AEController : MonoBehaviour 
{
    public float currentAE = 0f;
    public float maxAE = 500f;

    void Update()
    {
        //Debug.Log($"Current AE: {currentAE:F2}");
    }

    // comes from WardController
    public void AddAE (float amountToAdd) 
    {
        currentAE = Mathf.Min(currentAE + amountToAdd, maxAE);
    }

    //comes from PlayerController
    public bool SpendAE (float amountToRemove) 
    {
        if (currentAE < amountToRemove)
        {
            return false;
        }

        currentAE -= amountToRemove;
        return true;
    }
}