using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject BasicSpell;
    public Rigidbody rb;

    public Transform cameraTransform;
    public float cameraAngle = 0f;

    Vector3 movement;
    Vector3 actualMovement;
    Vector3 normal;

    public float speed = 5f;
    public float sensitivity = 0.1f;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;;
    }

    void Update ()
    {        
        //Mouse related code
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            //later add settings screen functionality to this etc.
        }
        transform.Rotate(0, mouseDelta.x * sensitivity, 0); // horizontal rotation
        cameraAngle = Mathf.Clamp(cameraAngle - mouseDelta.y * sensitivity, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraAngle, 0, 0); // vertical rotation
    
        //spell related code
        Vector3 spellSpawnPoint = transform.position + transform.forward * 2f;
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(BasicSpell, spellSpawnPoint, transform.rotation);
        }

        //movement related code
        movement = Vector3.zero;
        if(Keyboard.current.wKey.isPressed) 
        {
            movement += transform.forward;
        }
        if(Keyboard.current.sKey.isPressed) 
        {
            movement -= transform.forward;
        }
        if(Keyboard.current.dKey.isPressed) 
        {
            movement += transform.right;
        }
        if(Keyboard.current.aKey.isPressed) 
        {
            movement -= transform.right;
        }
        if (movement != Vector3.zero)
        {
            movement.Normalize();
        }
    }
    
    void OnCollisionStay (Collision collision)
    {
        normal = collision.contacts[0].normal;
    }
    
    void FixedUpdate ()
    {
        if(Vector3.Dot(movement, normal) <= 0)
        {
            actualMovement = Vector3.ProjectOnPlane(movement, normal);
            rb.linearVelocity = actualMovement * speed;
        }
        else {
            rb.linearVelocity = movement * speed; // physics movement
        }
    }
}