using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //update this variable after every 2 seconds
        private float playerSpeed = GameManager.playerSpeed; // Speed of the spaceship movement

        private float minY = -4.5f; // Minimum Y position (adjust according to camera boundary)
        private float maxY = 4.5f; // Maximum Y position (adjust according to camera boundary)

        private float shootInterval = 0.5f; // Time interval between player bullet spawns
        public GameObject playerBulletPrefab; // Reference to the player bullet prefab

        private float shootTimer = 0f;

    public void Start()
    {
        // Start the coroutine to update the playerSpeed every 2 seconds
        StartCoroutine(UpdatePlayerSpeed());
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void MoveUp()
    {
        float newYPosition = transform.position.y + (playerSpeed * Time.deltaTime);
        newYPosition = Mathf.Clamp(newYPosition, minY, maxY);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    public void MoveDown()
    {
        float newYPosition = transform.position.y - (playerSpeed * Time.deltaTime);
        newYPosition = Mathf.Clamp(newYPosition, minY, maxY);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    public void Shoot()
    {
        if (shootTimer >= shootInterval)
        {
            InstantiatePlayerBullet();         
            shootTimer = 0f;
        }
    }

    private void InstantiatePlayerBullet()
    {
        // Instantiate the enemy bullet at the spaceship's position
        Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator UpdatePlayerSpeed()
    {
        while (true)
        {
            // Update the playerSpeed value every 2 seconds
            playerSpeed = GameManager.playerSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}