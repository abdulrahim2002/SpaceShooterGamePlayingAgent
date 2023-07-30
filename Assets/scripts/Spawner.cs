using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    private float spawnInterval = GameManager.spawnInterval; // Time interval between spawning rocks and enemy spaceships
    private float timer = 0f;

    public void Start()
    {
        SpawnPlayer();
        StartCoroutine(UpdateSpawnInterval());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    private void SpawnObject()
    {
        bool spawnRock = Random.value < 0.7f; // 70% chance to spawn a rock, 30% chance to spawn an enemy spaceship

        if (spawnRock)
        {
            SpawnRock();
        }
        else
        {
            SpawnEnemySpaceship();
        }
    }

    public void SpawnPlayer()
    {
        // Spawn the player at the desired position, restart method will call from GameManager.cs
        Vector3 spawnPosition =  new Vector3(-8f, 0f, 0f);
        Instantiate(playerPrefab, spawnPosition, Quaternion.Euler(0f, 0f, -90f));
    }

    private void SpawnRock()
    {
        float randomY = Random.Range(-4.5f, 4.5f); // Random y position within the desired range
        Vector3 spawnPosition = new Vector3(10f, randomY, 0f);
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnEnemySpaceship()
    {
        float randomY = Random.Range(-4.5f, 4.5f); // Random y position within the desired range
        Vector3 spawnPosition = new Vector3(10f, randomY, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0f, 0f, 90f));
    }

    private IEnumerator UpdateSpawnInterval()
    {
        while (true)
        {
            // Update the spawnInterval value every 2 seconds
            spawnInterval = GameManager.spawnInterval;

            yield return new WaitForSeconds(0.1f);
        }
    }
}