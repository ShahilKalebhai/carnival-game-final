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
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float holdTime = 0.5f; // Time in seconds to hold before moving

    private int bulletsShot = 0;
    private float timer = 0f;
    private bool isMoving = false;

    private void Start()
    {
        ValidateComponents();
        UpdateBulletsRemainingText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timer = Time.time; // Record the time when the button is pressed.
        }

        if (Input.GetMouseButton(0))
        {
            if ((Time.time - timer > holdTime) && !isMoving)
            {
                // The mouse button is held down for more than holdTime seconds and player is not moving.
                isMoving = true; // Set the flag that the player is moving.
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isMoving)
            {
                // If the player was moving, stop the movement because the mouse button is released.
                isMoving = false;
            }
            else if (bulletsShot < maxBullets)
            {
                // The mouse button was released before holdTime seconds, shoot a bullet.
                ShootBullet();
            }
            timer = 0f; // Reset the timer.
        }

        if (isMoving)
        {
            // If the player is supposed to be moving, move forward.
            transform.position += Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }

        // Check if all bullets have been shot.
        if (bulletsShot >= maxBullets)
        {
            RestartGame();
        }
    }

    private void ShootBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, Camera.main.transform.position + bulletOffset, Quaternion.identity);
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

    private void ValidateComponents()
    {
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
    }
}
