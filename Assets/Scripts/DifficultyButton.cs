using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int times;

    // Start is called before the first frame update
    void Start()
    {
        // initialize button
        button = GetComponent<Button>();

        // add listener to button
        button.onClick.AddListener(setSpeed);

        // initialize game Manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // method to set speed of game
    void setSpeed()
    {
        Debug.Log(button.gameObject.name + " was clicked.");

        // start the game
        gameManager.startGame(times);
    }
}
