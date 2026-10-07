using UnityEngine;

public class spellController : MonoBehaviour
{
    public float spellSpeed = 75f;
    public float spellLifetime = 5f;
    
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

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}