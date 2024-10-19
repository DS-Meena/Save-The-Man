using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // declare speed and score
    public int Health = 100;
    public bool isGameActive = true;
    
    public float speed = 50.0f;
    private float[] xRange = { -34, 42 };

    public bool speedPowerUp = false;
    public bool UmbrellaPowerUp = false;

    private Rigidbody playerRb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // initialize rigid boyd
        playerRb = GetComponent<Rigidbody>();

        // initialize game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        constrinMovement();

        // detect if player dies
        if (Health <= 0)
        {
            isGameActive = false;

            gameManager.gameOver();
            Debug.Log("Player died!");
        }
    }

    // move the player according to input
    void MovePlayer()
    {
        if (isGameActive)
        {
            // get player input
            float horizontalInput = Input.GetAxis("Horizontal");

            // move using translation
            transform.Translate(Vector3.right * -horizontalInput * Time.deltaTime * speed);
        }
    }

    // constraint the movement of player
    void constrinMovement()
    {
        // constraint the player movement
        if (transform.position.x > xRange[1])
        {
            transform.position = new Vector3(xRange[1], transform.position.y, transform.position.z);
        }
        if (transform.position.x < xRange[0])
        {
            transform.position = new Vector3(xRange[0], transform.position.y, transform.position.z);
        }
    }

    // check collisions with other Objects
    private void OnTriggerEnter(Collider other)
    {
        // check if collided with enemy
        if (other.gameObject.CompareTag("Enemy") && isGameActive)
        {
            // decrease health if no protection
            if (!UmbrellaPowerUp)
            {
                Debug.Log("Health decreases by 10");
                Health -= 10;

                // update health
                gameManager.updateHealth(Health);
            }

            Destroy(other.gameObject);
        }

        // check if collided with powerups
        if (other.gameObject.CompareTag("Speed") && isGameActive)
        {
            Debug.Log("speed increases by 50");
            speed += 50;
            speedPowerUp = true;

            Destroy(other.gameObject);

            // start coroutine
            StartCoroutine(speedPowerUpCountdown());
        }
        else if (other.gameObject.CompareTag("Health") && isGameActive)
        {
            Debug.Log("Health increases by 50");
            Health += 50;

            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Protect") && isGameActive)
        {
            Debug.Log("Protect the player for sometime");
            UmbrellaPowerUp = true;

            Destroy(other.gameObject);

            // start coroutine
            StartCoroutine(umbrellaPowerUpCountdown());
        }
    }

    IEnumerator speedPowerUpCountdown()
    {
        yield return new WaitForSeconds(10);

        // remove powerup
        speedPowerUp = false;
        speed -= 50;
    }

    IEnumerator umbrellaPowerUpCountdown()
    {
        yield return new WaitForSeconds(10);

        // remove protection
        UmbrellaPowerUp = false;
    }
}
