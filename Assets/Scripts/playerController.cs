using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject BasicSpell;
    public GameObject BasicWard;
    public WardController wardController;
    public GameObject Wardstone;
    public Rigidbody rb;
    public LayerMask groundLayer;

    public Transform cameraTransform;
    public float cameraAngle = 0f;

    Vector3 movement;
    Vector3 actualMovement;
    Vector3 wallNormal;

    public float verticalAimOffset = 2f;
    public float speed = 5f;
    public float sensitivity = 0.1f;
    public float spellSpeed = 75f;    
    public float placementTolerance = 2f;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

        // raycast related code
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo)){
            BaseController baseController = hitInfo.collider.gameObject.GetComponent<BaseController>();
            wardController = BasicWard.GetComponent<WardController>();
            if(baseController != null 
            && baseController.isInteractable == true 
            && Keyboard.current.eKey.wasPressedThisFrame
            ){ 
                if(BasicWard.activeSelf == false){
                    wardController.ActivateWard();
                }
                else if (BasicWard.activeSelf == true){
                    wardController.DeactivateWard();
                }
            }
        }
    
        //spell related code | Spell firing
        Vector3 spellSpawnPoint = transform.position + transform.forward * 2f;

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject spawnedSpell = Instantiate(BasicSpell, spellSpawnPoint, transform.rotation);
            Rigidbody spellRb = spawnedSpell.GetComponent<Rigidbody>();
            Vector3 spellDirection = cameraTransform.rotation * Quaternion.Euler(-verticalAimOffset,0,0) * Vector3.forward;
            spellRb.linearVelocity = spellSpeed * spellDirection;
        }

        //ward related code | Wardstone placement
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = new Ray(
                cameraTransform.position,
                cameraTransform.forward
            );

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                Vector3 offset = hit.point - wardController.baseTransform.position;
                offset.y = 0f;
                Vector3 wardstoneSpawnpoint = new Vector3 (hit.point.x, hit.point.y+1,hit.point.z); 

                float distance = offset.magnitude;

                if (wardController.wardstones.Count == 0 || Mathf.Abs(distance - wardController.wardRadius) <= placementTolerance)
                {
                    if (!wardController.CanPlaceWardstone(hit.point))
                    {
                        Debug.Log("Too close to another wardstone!");
                        return;
                    }
                    GameObject placedWardstone = Instantiate(Wardstone, wardstoneSpawnpoint, Quaternion.identity);
                    WardstoneController stoneController = placedWardstone.GetComponent<WardstoneController>();
                    wardController.RegisterWardstone(stoneController);
                }
            }
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
        wallNormal = collision.contacts[0].normal;
    }
    
    void FixedUpdate ()
    {
        if(Vector3.Dot(movement, wallNormal) <= 0)
        {
            actualMovement = Vector3.ProjectOnPlane(movement, wallNormal);
            rb.linearVelocity = actualMovement * speed;
        }
        else {
            rb.linearVelocity = movement * speed; // physics movement
        }
    }
}