using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Members
    [Header("Timer")]
    public Text timerScreen;
    float timer = 0.0f;
    public int timeLeft;
    bool stopTimer;
    public PlayerEngine player;

    [Header("Panels")]
    public GameObject timesUpPanel;   // active when time runs out
    public GameObject gameOverPanel;  // active when player dies
    public GameObject youWonPanel;    // active when all enemies are dead

    [Header("Enemies Counter")]
    public int totalEnemies;
    int deadEnemiesCounter;
    public Text deadEnemiesScreen;
    public Text totalEnemiesScreen;

    [Header("Tutorial")]
    public GameObject skipTutorialBTN;

    public void AddToDeadEnemyCounter()
    {
        deadEnemiesCounter++;
    }

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        deadEnemiesCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Texts
        deadEnemiesScreen.text = deadEnemiesCounter.ToString();
        totalEnemiesScreen.text = totalEnemies.ToString();

        if (!stopTimer) 
        {
            timer += Time.deltaTime;
            int secondsPassed = (int)timer;
            timerScreen.text = (timeLeft - secondsPassed).ToString();

            // timers up
            if ((timeLeft - secondsPassed) <= 0)
            {
                stopTimer = true;
                player.KillPlayer();
                timerScreen.text = "0";
                timesUpPanel.gameObject.SetActive(true);
            }
        }

        // player is dead
        if (player.playerDied && !stopTimer)
        {
            gameOverPanel.gameObject.SetActive(true);
            stopTimer = true;
        }

        // player wins, all enemies are dead
        if(deadEnemiesCounter == totalEnemies)
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                skipTutorialBTN.gameObject.SetActive(true);
            }
            else
            {
                youWonPanel.gameObject.SetActive(true);
            }
            stopTimer = true;
        }

    }
}