using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private float speedRock = GameManager.rockSpeed; // Speed of the rock movement

    private void Start()
    {
        // Start the coroutine to update the enemySpeed every 2 seconds
        StartCoroutine(UpdateRockSpeed());
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.left * speedRock * Time.deltaTime);
    }

    private IEnumerator UpdateRockSpeed()
    {
        while (true)
        {
            // Update the enemySpeed value every 2 seconds
            speedRock = GameManager.rockSpeed;

            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            GameManager.rocksDestroyed++;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }


}