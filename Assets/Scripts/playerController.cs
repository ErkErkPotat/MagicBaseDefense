using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    void Start ()
    {

    }

    void Update ()
    {
        if(Keyboard.current.wKey.isPressed) 
        {
            transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        }
        if(Keyboard.current.sKey.isPressed) 
        {
            transform.position += new Vector3(0, 0, -1) * speed * Time.deltaTime;
        }
        if(Keyboard.current.dKey.isPressed) 
        {
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        if(Keyboard.current.aKey.isPressed) 
        {
            transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        } 
    }
}