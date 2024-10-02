using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;


    [Header("Camera Settings")]
    public Camera FirstPersonComera;
    public Camera thirdPersonCamera;
    public float mouseSenesitivity = 2.0f;

    public float radius = 5.0f;
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;

    private float thete = 0.0f;
    private float phi = 0.0f;
    private float targetVericalRotion= 0;
    private float verticalRoatationSpeed = 240f;

    private bool isFirstPerson = true;
    private bool isGrounded;
    private Rigidbody rb:

     void Start()
     {
        rd = GetComponent<Rigidbody>()

        Cursor.lockState = CursorLockMode.Locked;
        SetupCameras();
        SetActiveCamera();
     
     }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
        HandleCameraToggle();
    }

    void SetupCameras()
    {
        firstPersonComera.transform.localPosition = new Vector3(0.0f,0.6f,0.0f);
        firstPersonComera.transform.localPosition = Qualernion.identity;
    }
    void SetActiveCamera()
    {
        firstPersonComera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }


    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        theta += mouseX;
        thete - Mathf.Repeat(theta, 360.0f);

        targetVericalRotion -= mouseY;
        targetVericalRotion = Mathf.Clamp(targetVericalRotion, yMinLimit, yMaxLimit);   
        phi = Mathf.MoveTowards(phi, targetVericalRotion, verticalRoatationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0f, thete, 0.0f);
        if (isFirstPerson)
        {
            firstPersonComera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);

            radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5, minRadius, maxRadius;
        }

        firstPersonComera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
    }
    void HandleCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
            SetActiveCamera();
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rd.AddForce(Vector3.up * jumpForce, ForceMode.lmpulse):
            isGrounded = false;
        }
    }
    void HandleMovement()
    {
        float movrHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        if (!isFirstPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 movement = cameraRight * moveHorizontal + cameraForward * MoveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
        else
        { 
            Vector3 moveMent = transtorm.right * moveHorizontal + transform.forward*MoveVertical;
            rb.MovePosition(rb.position + moveMent * moveSpeed * Time.deltaTime);
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    
}
