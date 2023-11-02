using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorChecker : MonoBehaviour
{
    public static UnityAction<int> BottleEnterFloorEvent;
    [SerializeField] private int bottleValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Debug.Log("Floor trigger entered by: " + other.name);
            if (BottleEnterFloorEvent != null) BottleEnterFloorEvent(bottleValue);
        }
    }

}
