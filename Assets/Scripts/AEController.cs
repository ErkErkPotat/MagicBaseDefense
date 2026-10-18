using UnityEngine;

public class AEController : MonoBehaviour 
{
    public float currentAE = 0f;
    public float maxAE = 10000f; //10k

    void Update()
    {
        Debug.Log($"Current AE: {currentAE:F2}");
    }

    public void AddAE (float amountToAdd) 
    {
        currentAE = Mathf.Min(currentAE + amountToAdd, maxAE);
    }

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