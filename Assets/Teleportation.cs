using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform[] teleportPoints; // Array to hold the teleport points
    private int currentPoint = 0; // To keep track of the current teleport point

    // Update is called once per frame
    void Update()
    {
        // Check for user input (e.g., press the 'T' key to teleport)
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        // Check if there are any teleport points
        if (teleportPoints.Length > 0)
        {
            // Teleport the character to the current teleport point
            transform.position = teleportPoints[currentPoint].position;

            // Update the current point index for the next teleport
            currentPoint = (currentPoint + 1) % teleportPoints.Length;
        }
    }
}
