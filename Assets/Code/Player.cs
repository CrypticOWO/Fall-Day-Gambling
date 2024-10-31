using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    public static bool InMenu = false;


    void Update()
    {
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
}
