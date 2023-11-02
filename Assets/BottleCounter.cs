using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottleCounter : MonoBehaviour
{
    private int bottleCount = 0;
    public Color[] skyboxColors = new Color[0]; // Initialize as an empty array
    private int currentColorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        BottleEnterScript.BottleEnterEvent += this.BottleIncrement;
        // Set initial skybox color if array is not empty
        if (skyboxColors.Length > 0) ChangeSkyboxColor();
    }

    public void BottleIncrement(int amt)
    {
        bottleCount += amt;
        GetComponent<TMP_Text>().text = "# Score: " + bottleCount;

        //changeif reached 5
        if (bottleCount % 5 == 0)
        {
            ChangeSkyboxColor();
        }
    }

    private void ChangeSkyboxColor()
    {
        if (skyboxColors.Length > 0)
        {
            RenderSettings.skybox.SetColor("_Tint", skyboxColors[currentColorIndex % skyboxColors.Length]);
            currentColorIndex++;
        }
        else
        {
            Debug.LogError("No colors specified in skyboxColors array");
        }
    }

    void OnDisable()
    {
        BottleEnterScript.BottleEnterEvent -= this.BottleIncrement;
    }
}
