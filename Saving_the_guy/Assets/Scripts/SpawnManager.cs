using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // create enemy and powerUps arrays
    public GameObject[] projectiles;
    public GameObject[] powerUps;

    // define spawn range
    private float[] xRange = { -34, 42 };
    private float[] yRange = { 50, 80 };
    private float zSpawn = 6.0f;

    // time inervals
    private float startDelay = 1.0f;
    private float EnemySpawnInterval = 3.0f;
    private float PowerUpSpawnInterval = 10.0f;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // initialize player controller
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function to spawn random projectile at random position
    void spawnRandomEnemy()
    {
        if (playerController.isGameActive)
        {
            // get random positions
            float randomX = Random.Range(xRange[1], xRange[0]);
            float randomY = Random.Range(yRange[0], yRange[1]);

            // get random enemy
            int randInt = Random.Range(0, projectiles.Length);

            // create spawn vector
            Vector3 spawnPos = new Vector3(randomX, randomY, zSpawn);

            // instantiate the enemy
            Instantiate(projectiles[randInt], spawnPos, projectiles[randInt].gameObject.transform.rotation);
        }
    }

    // function to spawn random powerup at random position
    void spawnRandomPowerUp()
    {
        if (playerController.isGameActive)
        {
            // get random positions
            float randomX = Random.Range(xRange[1], xRange[0]);
            float randomY = Random.Range(yRange[0], yRange[1]);

            // get random powerup
            int randInt = Random.Range(0, powerUps.Length);

            // create spawn vector
            Vector3 spawnPos = new Vector3(randomX, randomY, zSpawn);

            // instantiate the enemy
            Instantiate(powerUps[randInt], spawnPos, powerUps[randInt].gameObject.transform.rotation);
        }
    }

    // start game method for swapning
    public void startGame(int times)
    {
        InvokeRepeating("spawnRandomEnemy", startDelay, EnemySpawnInterval / times);
        InvokeRepeating("spawnRandomPowerUp", startDelay, PowerUpSpawnInterval);
    }
}
