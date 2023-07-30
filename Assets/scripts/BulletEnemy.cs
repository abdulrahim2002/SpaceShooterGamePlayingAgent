using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{   
    //update this variable after every 2 seconds
    private float enemyBulletSpeed = GameManager.enemyBulletSpeed; // Speed of the bullet movement
    
    private void Start()
    {
        // Start the coroutine to update the enemyBulletSpeed every 2 seconds
        StartCoroutine(UpdateEnemyBulletSpeedAndCheck());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(-1f, 0f, 0f) * enemyBulletSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private IEnumerator UpdateEnemyBulletSpeedAndCheck()
    {
        while (true)
        {
            // Update the enemyBulletSpeed value every 2 seconds
            enemyBulletSpeed = GameManager.enemyBulletSpeed;

            //destroy the bullet if it moves beyond -10f in x
            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}