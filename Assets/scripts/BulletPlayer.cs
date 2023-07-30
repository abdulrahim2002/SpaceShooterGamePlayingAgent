using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private float playerBulletSpeed = GameManager.playerBulletSpeed; // Speed of the bullet movement

    private void Start()
    {
        // Start the coroutine to update the enemyBulletSpeed every 2 seconds
        StartCoroutine(UpdatePlayerBulletSpeedAndCheck());
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movement = new Vector3(1f, 0f, 0f) * playerBulletSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private IEnumerator UpdatePlayerBulletSpeedAndCheck()
    {
        while (true)
        {
            // Update the enemyBulletSpeed value every 2 seconds
            playerBulletSpeed = GameManager.playerBulletSpeed;
            
            //destroy the bullet if it moves beyond 10f in x
            if (transform.position.x > 10f)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

}
