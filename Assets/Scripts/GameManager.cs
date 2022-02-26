using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject titleScreen;

    private int score;
    private PlayerController playerController;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // initialize player controller
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // initialize spawn Manager object
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // method to show it's game over
    public void gameOver()
    {
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // method to update health
    public void updateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }

    // thread to update scores
    IEnumerator UpdateScores(int times)
    {
        while(playerController.isGameActive)
        {
            yield return new WaitForSeconds(1f/times);

            score++;

            // update text
            scoreText.text = "Score: " + score;
        }
    }

    // method to reload the scene
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // method to start the game
    public void startGame(int times)
    {
        score = 0;

        // start updating scores and health
        StartCoroutine(UpdateScores(times));
        updateHealth(100);

        // disable title screen
        titleScreen.gameObject.SetActive(false);

        // call start game of spawn Manager
        spawnManager.startGame(times);
    }
}
