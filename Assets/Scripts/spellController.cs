using UnityEngine;

public class spellController : MonoBehaviour
{
    public float spellSpeed = 75f;
    public float spellLifetime = 5f;
    public float spellDamage = 25f;
    
    void Start ()
    {
        Invoke("DestroySpell", spellLifetime);
    }

    void Update ()
    {
        transform.position += transform.forward * spellSpeed * Time.deltaTime; //spell movement
    }

    void DestroySpell ()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        
        Destroy(gameObject);
    }
}