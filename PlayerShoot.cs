using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 1000f;
    [SerializeField] private Vector3 bulletOffset;
    [SerializeField] private int maxBullets = 10;
    [SerializeField] private TMP_Text bulletsRemainingText;

    private int bulletsShot = 0;

    private void Start()
    {
        // Basic null checks to prevent issues right from the start
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab is not assigned in the inspector!");
            enabled = false; // Disable this script to prevent further issues
            return;
        }

        if (bulletsRemainingText == null)
        {
            Debug.LogError("bulletsRemainingText is not assigned in the inspector!");
            enabled = false; // Disable this script to prevent further issues
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("Main Camera is not found in the scene!");
            enabled = false; // Disable this script to prevent further issues
            return;
        }

        UpdateBulletsRemainingText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletsShot < maxBullets)
        {
        
            GameObject newBullet = Instantiate(bulletPrefab, Camera.main.transform.position + bulletOffset, Quaternion.identity, Camera.main.transform);

            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Camera.main.transform.forward * bulletForce);
            }
            else
            {
                Debug.LogError("BulletPrefab does not have a Rigidbody component!");
            }
            bulletsShot++;
            UpdateBulletsRemainingText();
        }

        // Check if all bullets have been shot
        if (bulletsShot >= maxBullets) 
        {
            RestartGame();
        }
    }

    private void UpdateBulletsRemainingText()
    {
        int bulletsRemaining = maxBullets - bulletsShot;
        bulletsRemainingText.text = "Bullets Remaining: " + bulletsRemaining;
    }

    private void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
