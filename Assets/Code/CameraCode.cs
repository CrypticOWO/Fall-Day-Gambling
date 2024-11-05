using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    public Rigidbody RB; // Use Rigidbody for movement
    public GameObject TargetPlayer;

    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    float cameraHorizontalRotation = 0f;
    public float speed = 10f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public static string TypeOfMenu = "NA";
    public static string InCutscene = "No";
    public static string LookingAt = "Nothing";
    public static string LockView = "No";

    void Start()
    {
        mouseSensitivity = 2f;
        speed = 10f;
        RB.isKinematic = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        TypeOfMenu = "Start";
        InCutscene = "No";
        LookingAt = "Nothing";
        LockView = "Yes";

        transform.position = new Vector3(8, 8, 26);
        TargetPlayer.transform.position = transform.position + new Vector3(0, -3, 0);
        transform.localEulerAngles = new Vector3(0, -120, 0);
    }

    void Update()
    {
        if (LockView == "No" && InCutscene == "No")
        {
            HandleCameraMovement();
        }

        if (Input.GetKeyDown(KeyCode.E) && LookingAt == "Table" && InCutscene == "No")
        {
            //Store Old Info for Exit
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            
            // Locking into the table animation
            StartCoroutine(PanToNewPosition(new Vector3(8, 7.8f, 70), Quaternion.Euler(90, 0, 0), 0.6f));

            //Table Code
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LockView = "Yes";
            //LeaveTable.AtTable = "Yes";
            RB.isKinematic = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && LookingAt == "Jukebox" && InCutscene == "No")
        {
            //Store Old Info for Exit
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            
            // Locking into the table animation
            StartCoroutine(PanToNewPosition(new Vector3(25, 6f, 60), Quaternion.Euler(0, 90, 0), 0.6f));

            //Table Code
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LockView = "Yes";
            //LeaveJukebox.AtJukebox = "Yes";
            RB.isKinematic = true;
        }
        if (LookingAt == "Nothing" && TypeOfMenu == "Casino" && InCutscene == "Yes" && LockView == "Yes")
        {
            StartCoroutine(PanToNewPosition(new Vector3(8, 8, 26), Quaternion.identity, 5f));
            LockView = "No";
        }

        if ((LeaveTable.AtTable == "No" || LeaveJukebox.AtJukebox == "No") && LockView == "Yes" && (LookingAt == "Table" || LookingAt == "Jukebox") && InCutscene == "No")
        {
            StartCoroutine(PanToNewPosition(originalPosition, originalRotation, 1f));
            LeaveTable.AtTable = "Yes";
            LeaveJukebox.AtJukebox = "Yes";
            LockView = "No";
            
        }
    }

    void HandleCameraMovement()
    {
        RB.isKinematic = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        cameraHorizontalRotation += inputX;
        transform.localEulerAngles = new Vector3(cameraVerticalRotation, cameraHorizontalRotation, 0);

        CheckForInteractables();

        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0; // Ignore the y component

            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

            // Create the final movement vector
            Vector3 moveDirection = (forward * direction.z + right * direction.x).normalized;

            // Move the player using Rigidbody
            RB.MovePosition(RB.position + moveDirection * speed * Time.deltaTime);
        }
    }

    IEnumerator PanToNewPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        InCutscene = "Yes";

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float time = 0f;

        // Hide cursor during cutscene
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        InCutscene = "No";

    }

    void CheckForInteractables()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        LookingAt = "Nothing";

        if (Physics.Raycast(ray, out RaycastHit hit, 7))
        {
            GameObject HitObject = hit.collider.gameObject;
            string HitTag = HitObject.tag;
            LookingAt = HitTag;
        }
    }
}
