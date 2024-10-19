using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonRise : MonoBehaviour
{
    private Rigidbody balloonRB;
    private GameManager gameManager;

    private float minForce = 2;
    private float maxForce = 4;
    private float xRange = 15;       
    private float zPos = -15;

    // create reference to particle system 
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        balloonRB = GetComponent<Rigidbody>();

        // initialize game manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // add initial force
        balloonRB.AddForce(randomForce(), ForceMode.Impulse);

        // set position
        transform.position = randomPos();
    }

    // Update is called once per frame
    void Update()
    {
        explosionParticle.transform.position = transform.position;
    }

    // on clicking on target
    [System.Obsolete]
    private void OnMouseDown()
    {
        // destroy objects only while game is active
        if (gameManager.isGameActive)
        {
            explosionParticle.Play();

            Destroy(gameObject, explosionParticle.duration);

            // now update score on deleting it
            gameManager.updateScore(1);
        }
    }

    // on colliding with trigger (here sensor has trigger)
    private void OnTriggerEnter(Collider other)
    {
        gameManager.lives--;
        gameManager.escapes();
        gameManager.updateLives();

        Destroy(gameObject);
    }

    // method to get random force
    Vector3 randomForce()
    {
        return Vector3.forward * Random.Range(minForce, maxForce);
    }

    // method to get random position
    Vector3 randomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), 1, zPos);
    }
}
