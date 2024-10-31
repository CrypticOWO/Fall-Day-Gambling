using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    public Transform TargetPlayer;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    float cameraHorizontalRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = 2f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = TargetPlayer.transform.position; new Vector3(2,3.5f,-10);

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        cameraHorizontalRotation += inputX;
        transform.localEulerAngles = new Vector3(cameraVerticalRotation, cameraHorizontalRotation, 0);
    
        //Detect interactables
        CheckForInteractables();
    }

    void CheckForInteractables()
    {
        // Create the ray from the camera's position in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 7))
        {
            if (hit.collider.gameObject.name == "Table")
            {
                Debug.Log("Time to PLAY");
            }
        }
    }   
}
