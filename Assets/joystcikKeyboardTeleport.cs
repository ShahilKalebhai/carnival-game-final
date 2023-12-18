using UnityEngine;

public class JoystickKeyboardMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 100.0f; // Speed for rotation
    public float teleportDistance = 10.0f; // Distance to teleport forward
    public LayerMask teleportLayer; // Layer mask to check for valid teleportation

    // Xbox controller mappings
    private const string XboxLeftBumper = "LB"; // or "Joystick Button 5" if needed
    private const string XboxLeftTrigger = "LB"; // Custom axis you need to set in the Input Manager for the Left Trigger

    private const string XboxRightBumper = "Joystick Button 14"; //
    private const string XboxRightTrigger = "RB"; 

    void Update()
    {
        // Movement with WASD or Joystick left stick
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Joystick left stick left/right
        float vertical = Input.GetAxis("Vertical"); // W/S or Joystick left stick up/down

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection = transform.TransformDirection(movementDirection);
        movementDirection.y = 0; // Prevent moving in the vertical direction

        transform.position += movementDirection * movementSpeed * Time.deltaTime;

        // Rotation with right joystick
        float rotationHorizontal = Input.GetAxis("RightStickHorizontal"); // Joystick right stick left/right
        float rotationVertical = Input.GetAxis("RightStickVertical");

        
        transform.Rotate(0, rotationHorizontal * rotationSpeed * Time.deltaTime, 0);

        // Teleportation with Spacebar or Xbox controller LB (L1)
        if ( Input.GetButton(XboxLeftBumper))
        {
            Teleport();
            Debug.Log("L1 Pressed");

        }
        // Check for LB press
        if (Input.GetButtonDown("LB"))
        {
            // Perform the action for LB
            Debug.Log("LB Pressed");
        }

        // Check for RB press
        if (Input.GetButtonDown("RB"))
        {
            // Perform the action for RB
            Debug.Log("RB Pressed");
        }

        // Check for LT press
        float ltValue = Input.GetAxis("LT");
        if (ltValue > 0.1f) // Use some threshold to account for trigger deadzone
        {
            // Perform the action for LT

        }

        // Check for RT press
        float rtValue = Input.GetAxis("RT");
        if (rtValue < -0.1f) // Use some threshold to account for trigger deadzone
        {
            // Perform the action for RT
        }

        // Teleportation with Xbox controller LT (L2) - Trigger value might need to be checked differently
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetAxis(XboxLeftTrigger) > 0.5f) // Assuming a press is registered above 0.5f
        {
            transform.position += transform.forward * teleportDistance;
        }

        // Teleportation with L1 or L2 or Alt keys
        // Replace "L1" and "L2" with the actual input names from your Input Manager
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetButtonDown("LB"))
        {
            transform.position += transform.forward * teleportDistance;
            Debug.Log("LB Pressed");
        }
        if (Input.GetKeyDown(KeyCode.RightAlt) || Input.GetButtonDown("RB"))
        {
            transform.position -= transform.forward * teleportDistance;
            Debug.Log("RB Pressed");
        }
    }


      private void Teleport()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, teleportDistance, teleportLayer))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position += transform.forward * teleportDistance;
        }
    }
}
