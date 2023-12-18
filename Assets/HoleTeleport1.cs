using UnityEngine;

public class HoleTeleport1 : MonoBehaviour
{
    public Transform teleportDestination; // Assign the teleport destination transform here
    public float gazeTime = 2.0f; // Time in seconds to gaze at the hole to teleport

    private float gazeTimer = 0f;
    private bool isGazing = false;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("holeTeleport"))
            {
                if (!isGazing)
                {
                    isGazing = true;
                    gazeTimer = 0f;
                }

                gazeTimer += Time.deltaTime;
                if (gazeTimer >= gazeTime)
                {
                    // Teleport the player
                    transform.position = teleportDestination.position;
                    gazeTimer = 0f;
                }
            }
            else
            {
                isGazing = false;
            }
        }
        else
        {
            isGazing = false;
        }
    }
}
