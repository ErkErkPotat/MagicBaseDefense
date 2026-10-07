using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject BasicSpell;

    public Transform cameraTransform;
    public float cameraAngle = 0f;

    public float speed = 5f;
    public float sensitivity = 0.1f;
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;;
    }

    void Update ()
    {
        Vector3 movement = Vector3.zero;
        Vector3 spellSpawnPoint = transform.position + transform.forward * 2f;
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
        transform.position += movement * speed * Time.deltaTime; // movement

        transform.Rotate(0, mouseDelta.x * sensitivity, 0); // horizontal rotation
        cameraAngle = Mathf.Clamp(cameraAngle - mouseDelta.y * sensitivity, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraAngle, 0, 0); // vertical rotation
    
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(BasicSpell, spellSpawnPoint, transform.rotation);
        }
    }
}