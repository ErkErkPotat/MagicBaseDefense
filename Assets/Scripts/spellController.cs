using UnityEngine;

public class SpellController : MonoBehaviour
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