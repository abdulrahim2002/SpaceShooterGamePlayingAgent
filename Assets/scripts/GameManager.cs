using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game Stastistics, accessed and updated in other scripts
        public static int playerHealth = 2; // Player health value
        public static int timeSurvived; // Player score value
        public static int rocksDestroyed = 0; // Number of rocks destroyed
        public static int enemyShipsDestroyed = 0; // Number of enemy ships destroyed

    // Variables used to regenerate health timer
        private float healthTimer = 0f;
        private float maxHealthTimer = 30f; // After 30 seconds health points added/health regenerated

    // Difficulty variables
        private static float timer = 0f;
        private float maxDuration = 300f; // Maximum duration of 5 minutes

        private float minDifficulty = 0.7f; // Minimum difficulty value
        private float maxDifficulty = 2.5f; // Maximum difficulty value

    // Game state variables, accessed in other scripts. 
        //BulletEnemy script
        public static float enemyBulletSpeed = 5f; // Speed of the bullet enemy's movement
        //BulletPlayer script
        public static float playerBulletSpeed = 10f; // Speed of the bullet movement
        //PlayerController script
        public static float playerSpeed = 5f; // Speed of the enemy movement
        //EnemyControllerscript
        public static float enemySpeed = 2.5f; // Speed of the enemy movement
        //spwaner script
        public static float spawnInterval = 2f; // Time interval between spawning rocks and enemy spaceships
        //RockController script
        public static float rockSpeed = 2f; // Speed of the rock movement
        //background script
        public static float scrollBackgroundSpeed = 1f; // Speed of the background scrolling

    private void Start()
    {
        // Start the coroutine to increase the difficulty until 5 minutes, after that this coroutine stops
        StartCoroutine(IncreaseDifficulty());
        StartCoroutine(RegenerateHealthAndUpdateScore());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        healthTimer += Time.deltaTime;
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            // Calculate the difficulty value based on the elapsed time
            float normalizedTime = Mathf.Clamp01(timer / maxDuration);
            float difficulty = Mathf.Lerp(minDifficulty, maxDifficulty, normalizedTime);

            // Apply the difficulty scaling to the variables
            enemyBulletSpeed = 9f * difficulty;
            playerBulletSpeed = 13f * difficulty;
            playerSpeed = 4f * difficulty;
            enemySpeed = 6f * difficulty;
            spawnInterval = 1f / difficulty;
            rockSpeed = 6f * difficulty;
            scrollBackgroundSpeed = 3f * difficulty;

            if (timer >= maxDuration)
            {
                // Stop the coroutine to increase the difficulty
                StopCoroutine(IncreaseDifficulty());
            }

            //wait 1 second before next difficulty update
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RegenerateHealthAndUpdateScore()
    {
        while (true)
        {
            timeSurvived = Mathf.FloorToInt(timer);

            if (playerHealth < 2 && healthTimer >= maxHealthTimer)
            {
                playerHealth++;
                healthTimer = 0f;
            }

            //wait 1 second before next difficulty update
            yield return new WaitForSeconds(0.1f);
        }
    }

    //resert the game statistics
    public static bool RestartGame()
    {
        Debug.Log("Game Restarted");

        playerHealth = 2;
        timeSurvived = 0;
        rocksDestroyed = 0;
        enemyShipsDestroyed = 0;

        timer = 0f;

        return true;

    }

}