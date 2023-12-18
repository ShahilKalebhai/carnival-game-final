using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTeleport : MonoBehaviour
{
   public float gazeTeleportTime = 3.0f; // Time in seconds to gaze before teleporting
public LayerMask teleportLayer; // Layers that are valid for teleporting
private float gazeTimer = 0.0f;
private Vector3 targetTeleportLocation;
private bool isGazing = false;

void Update()
{
    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayer))
    {
        if (!isGazing)
        {
            isGazing = true;
            targetTeleportLocation = hit.point;
            gazeTimer = 0.0f;
        }
        else
        {
            gazeTimer += Time.deltaTime;
            if (gazeTimer >= gazeTeleportTime)
            {
                transform.position = targetTeleportLocation; // Teleport to the location
                gazeTimer = 0.0f;
            }
        }
    }
    else
    {
        isGazing = false;
        gazeTimer = 0.0f;
    }
}

}
