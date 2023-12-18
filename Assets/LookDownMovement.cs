using UnityEngine;

public class CardboardPlayerMovement : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed
    public float lookDownThreshold = 30f; // Angle threshold to start moving

    void Update()
    {
        // Get the camera's rotation
        Quaternion cameraRotation = Camera.main.transform.rotation;

        // Convert rotation to Euler angles for easier comparison
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // Check if the player is looking down
        float xAngle = cameraEulerAngles.x;
        // Adjust for Euler angles' 0-360 wrap-around
        if (xAngle > 180) xAngle -= 360;

        // Move the player if they are looking down
        if (xAngle >= lookDownThreshold && xAngle < 90.0f)
        {
            // Forward direction in VR is along the camera's local Z-axis
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0; // Eliminate vertical movement

            transform.position += forward * speed * Time.deltaTime;
        }
    }
}
