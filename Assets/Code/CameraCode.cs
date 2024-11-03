using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    //public Transform TargetPlayer;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    float cameraHorizontalRotation = 0f;
    public float speed = 5f;

    public static string LookingAt = "Nothing";
    public static string LockView = "No";
    
    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = 2f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        LookingAt = "Nothing";
        LockView = "Yes";
    }

    // Update is called once per frame
    void Update()
    {
        if (LockView == "No")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //transform.position = TargetPlayer.transform.position; new Vector3(2,3.5f,-10);

            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

            cameraHorizontalRotation += inputX;
            transform.localEulerAngles = new Vector3(cameraVerticalRotation, cameraHorizontalRotation, 0);
    
            //Detect interactables
            CheckForInteractables();

            // Get input from the player
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Create a movement vector
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            // Move relative to the player's forward direction
            if (direction.magnitude >= 0.1f)
            {
                // Get the player's forward direction
                Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
                forward.y = 0; // Ignore the y component

                // Calculate the right direction
                Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

                // Create the final movement vector
                Vector3 moveDirection = (forward * direction.z + right * direction.x).normalized;

                // Move the player
                transform.position += moveDirection * speed * Time.deltaTime;
        }
        }
        if (Input.GetKeyDown(KeyCode.E) && LookingAt == "Table") // Check if the "E" key is pressed
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Code for when player is looking at object tagged "balls"
            LockView = "Yes";
            transform.position = new Vector3(0, 7.5f, 20);
            transform.localEulerAngles = new Vector3(90, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.P) && LockView == "Yes")
        {
            LockView = "No";
            transform.position = new Vector3(0, 8, 5);
            transform.localEulerAngles = new Vector3(cameraVerticalRotation, cameraHorizontalRotation, 0);
        }
    }

    void CheckForInteractables()
    {
        // Create the ray from the camera's position in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);

        LookingAt = "Nothing";

        if (Physics.Raycast(ray, out RaycastHit hit, 10))
        {
            if (hit.collider.gameObject.name == "Table")
            {
                //Debug.Log("Time to PLAY");
                LookingAt = "Table";
            }
        }
    }   
}
