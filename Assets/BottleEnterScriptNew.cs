using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottleEnterScript : MonoBehaviour
{
    public static UnityAction<int> BottleEnterEvent;
    [SerializeField] private int bottleWorth = 1;
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger");
        if(other.CompareTag("Floor")) {
            if (BottleEnterEvent != null) BottleEnterEvent(bottleWorth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
