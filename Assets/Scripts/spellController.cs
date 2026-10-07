using UnityEngine;

public class spellController : MonoBehaviour
{
    public float spellSpeed = 15f;
    public float spellLifetime = 3f;
    
    void Start ()
    {
        Invoke("destroySpell", spellLifetime);
    }

    void Update ()
    {
        transform.position += transform.forward * spellSpeed * Time.deltaTime; //spell movement
    }

    void destroySpell ()
    {
        Destroy(gameObject);
    }
}