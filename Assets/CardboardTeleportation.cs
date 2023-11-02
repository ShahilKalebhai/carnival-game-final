using UnityEngine;

public class CardboardTeleportation : MonoBehaviour
{
    public Transform[] teleportPoints; // Array to hold the teleport points
    public float gazeTime = 2.0f; // Time in seconds to gaze at the point before teleporting
    public LayerMask teleportLayer; // Layer mask to filter teleportable objects

    private float gazeTimer = 0.0f;
    private Transform currentGazedPoint = null;
    private bool isGazingAtTeleportPoint = false;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayer))
        {
            if (hit.collider.CompareTag("TeleportPoint"))
            {
                if (currentGazedPoint != hit.collider.transform)
                {
                    gazeTimer = 0.0f;
                    currentGazedPoint = hit.collider.transform;
                    HighlightPoint(currentGazedPoint);
                }
                gazeTimer += Time.deltaTime;

                if (gazeTimer >= gazeTime)
                {
                    isGazingAtTeleportPoint = true;
                }
            }
        }
        else
        {
            gazeTimer = 0.0f;
            currentGazedPoint = null;
            isGazingAtTeleportPoint = false;
        }

        // Check for input (click or tap)
        if (Input.GetButtonDown("Fire1") && isGazingAtTeleportPoint)
        {
            Teleport(currentGazedPoint);
            isGazingAtTeleportPoint = false;
        }
    }

    void Teleport(Transform point)
    {
        transform.position = point.position;
    }

    void HighlightPoint(Transform point)
    {
        // Implement point highlighting logic here
        // E.g., change the material, play an animation, etc.
        Debug.Log("Gazing at point: " + point.name);
    }
}
