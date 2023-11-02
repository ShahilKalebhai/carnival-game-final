using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BottleCounterClass : MonoBehaviour
{
    private int bottleCount = 0;
    [SerializeField] private List<AudioClip> bottleSounds;
    // Start is called before the first frame update
    void Start()
    {
        FloorChecker.BottleEnterFloorEvent += this.BottleIncrement;
    }

    public void BottleIncrement(int amt) {
        bottleCount += amt;
        GetComponent<TMP_Text>().text = $"# Bottles: {bottleCount}";
        if (amt > 1)
        {
            GetComponent<AudioSource>().clip = bottleSounds[1];
        } else {
            GetComponent<AudioSource>().clip = bottleSounds[0];
        } 
        GetComponent<AudioSource>().Play(0);
    }

    void OnDisable() {
        FloorChecker.BottleEnterFloorEvent -= this.BottleIncrement;
    }
}
