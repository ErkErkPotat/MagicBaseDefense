using UnityEngine;

public class WardstoneController : MonoBehaviour
{
    public WardController parentWard;

    public void RemoveWardstone ()
    {
        Destroy(gameObject);
    }
}