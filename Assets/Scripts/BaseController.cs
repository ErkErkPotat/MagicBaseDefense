using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float baseMaxHealth = 1000f;
    public float baseCurrentHealth = 1000f;

    public bool isInteractable = true;

    void Start      (){}
    void Update     (){}

    public void TakeDamage (float damage)
    {
        baseCurrentHealth -= damage;
        if(baseCurrentHealth <= 0)
        {
            //handle game loss screen.
            Debug.Log("You Lost :(");
            Time.timeScale = 0f;
        }
        Debug.Log("Base took " + damage + " damage. Health: " + baseCurrentHealth);
    }
}