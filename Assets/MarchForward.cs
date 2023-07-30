using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MarchForward : Agent
{
    private PlayerController controls;

    /*-----------------------------------------*/
    // these lists will be used to store the positions of the objects in the scene, and act as observations
    public List<Vector3> enemyShipPositions;
    public List<Vector3> enemyBulletPositions;
    public List<Vector3> rockPositions;
    /*-----------------------------------------*/

    public enum Labels
    {
        // after adding each observation to the sensor, we need to label it with a channel name
        enemyShipChannel,
        enemyBulletChannel,
        rockChannel
    }

    public override void OnEpisodeBegin()
    {
        controls = GetComponent<PlayerController>();
        // reset game stastics
        GameManager.RestartGame();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Clear the position lists
        enemyShipPositions.Clear();
        enemyBulletPositions.Clear();
        rockPositions.Clear();

        // Collect positions for each object type
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("villain"))
        {
            enemyShipPositions.Add(obj.transform.position);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("enemyBullet"))
        {
            enemyBulletPositions.Add(obj.transform.position);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("rock"))
        {
            rockPositions.Add(obj.transform.position);
        }


        /*--------------------------------------------------------------------------------------------*/

        // Add the positions to the observation space using separate channels
        sensor.AddObservation(transform.position);                           // agents position

        foreach (Vector3 position in rockPositions)                          // rock positions
        {
            sensor.AddObservation(position);
            sensor.AddObservation((int)Labels.rockChannel);
        }

        foreach (Vector3 position in enemyShipPositions)                     // enemy posirion
        {
            sensor.AddObservation(position);
            sensor.AddObservation((int)Labels.enemyShipChannel);
        }

        foreach (Vector3 position in enemyBulletPositions)                  // enemt bullet position
        {
            sensor.AddObservation(position);
            sensor.AddObservation((int)Labels.enemyBulletChannel);
        }

        /*---------------------------------------------------------------------------------------------*/

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //// outputs are discrete actions, either move up, move down or shoot.
       int action = actions.DiscreteActions[0];

        // Perform an action based on the chosen action value
        switch (action)
        {
            case 0:
                // Move up
                controls.MoveUp();
                break;
            case 1:
                // Move down
                controls.MoveDown();
                break;
            case 2:
                // Shoot
                controls.Shoot();
                break;
            case 3:
                // do nothing
                break;
            default:
                // Invalid action, do nothing or handle error
                break;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // working
        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            discreteAction[0] = 0;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            discreteAction[0] = 1;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            discreteAction[0] = 2;
        }
        else
        {
            discreteAction[0] = 3;
        }

    }

    private void OnTriggerEnter2D(Collider2D triggerCollidor)
    {
        // tags: enemyBullet, rock, villain
        if (triggerCollidor.tag == "enemyBullet")
        {
            Destroy(triggerCollidor.gameObject);
            //decrease health

            GameManager.playerHealth -= 1;
            if (GameManager.playerHealth <= 0)
            {
                //Destroy(gameObject);

                // give reward which is its score
                // AddReward(GameManager.timeSurvived + 3f * GameManager.rocksDestroyed + 5f * GameManager.enemyShipsDestroyed);
                AddReward(GameManager.timeSurvived);
                // make bg red
                //backgroundSpriteRenderer
                // end episode
                EndEpisode();
            }
        }

        if (triggerCollidor.tag == "rock")
        {
            Destroy(triggerCollidor.gameObject);
            //Destroy(gameObject);

            // give reward which is its score
            AddReward(GameManager.timeSurvived + 3f * GameManager.rocksDestroyed + 5f * GameManager.enemyShipsDestroyed);
            // make bg red
            // end episode
            EndEpisode();
        }

        if (triggerCollidor.tag == "villain")
        {
            Destroy(triggerCollidor.gameObject);
            //Destroy(gameObject);

            // give reward which is its score
            AddReward(GameManager.timeSurvived + 3f * GameManager.rocksDestroyed + 5f * GameManager.enemyShipsDestroyed);
            // make bg red
            // end episode
            EndEpisode();
        }

    }

}
