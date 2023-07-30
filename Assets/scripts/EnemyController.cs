using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float enemySpeed = GameManager.enemySpeed; // Speed of the enemy movement
    public GameObject enemyBulletPrefab; // Reference to the enemy bullet prefab

    private float xCoordinate; // Random x coordinate for shooting bullets

    void Start()
    {
        // Generate a random x coordinate between 0 and 5.5 for each enemy ship
        xCoordinate = Random.Range(0f, 5.5f);
        // Start the coroutine to update the enemySpeed every 2 seconds
        StartCoroutine(UpdateEnemySpeed());
        StartCoroutine(CheckIfToShoot());
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.up * enemySpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        //instantiate enemyBullet
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
    }


    private IEnumerator UpdateEnemySpeed()
    {
        while (true)
        {
            // Update the enemySpeed value every 2 seconds
            enemySpeed = GameManager.enemySpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CheckIfToShoot()
    {
        while (true)
        {
            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
            }

            // If the enemy ship is close to the x coordinate, it shoots a bullet
            if (Mathf.Abs(transform.position.x - xCoordinate) < 0.3f)
            {
                Shoot();
            }
            yield return new WaitForSeconds(0.2f) ;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            GameManager.enemyShipsDestroyed++;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
