using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject balloon;
    public bool isGameActive;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI lifeStatus;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject HomeScreen;
    public int lives;
    public AudioClip burstSound;
    public AudioClip escapeSound;

    private AudioSource playerAudio;
    private float spawnRate = 1.0f;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        // audio source
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // escape sound
    public void escapes()
    {
        // burst sound
        if (isGameActive)
            playerAudio.PlayOneShot(escapeSound, 1.0f);
    }

    // method to reload current scene
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // method to show it's game over
    public void gameOver()
    {
        isGameActive = false;

        // show game over text and restart button
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // create method to update score
    public void updateLives()
    {
        // set new score
        lifeStatus.text = "Lives: ";
        if (lives == 3)
            lifeStatus.text += "❤❤❤";
        else if (lives == 2)
            lifeStatus.text += "❤❤";
        else if (lives == 1)
            lifeStatus.text += "❤";
        else
            gameOver();
    }

    // create method to update score
    public void updateScore(int scoreToAdd)
    {
        count += scoreToAdd;

        // set new score
        countText.text = "Count: " + count;

        // burst sound
        if (count > 0)
            playerAudio.PlayOneShot(burstSound, 1.0f);
    }

    // create spawn coroutine
    IEnumerator spawnTargets()
    {
        // spawn objects only while game is active
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);

            // initiate random target
            Instantiate(balloon);
        }
    }

    // method to start game
    public void startGame()
    {
        isGameActive = true;
        count = 0;
        lives = 3;

        updateScore(0);
        updateLives();

        // start spawncoroutine
        StartCoroutine(spawnTargets());

        // disable the title screen
        HomeScreen.gameObject.SetActive(false);

        // enable count and lives
        countText.gameObject.SetActive(true);
        lifeStatus.gameObject.SetActive(true);
    }
}
