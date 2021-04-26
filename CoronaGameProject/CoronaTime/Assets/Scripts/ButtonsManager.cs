using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonsManager : MonoBehaviour
{
    //Members
    [Header("Player")]
    public PlayerEngine player;
    [Header("Panels")]
    public GameObject escapePanel;
    public GameObject tutorialPanel;
    [Header("Texts")]
    public Text aboutTheGameText;
    public Text howToPlayText;
    public Text aboutUsText;
    [Header("Sound")]
    public AudioSource sorce;
    public AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayerControl(string command)
    {
        if (command == "Right")
        {
            player.MoveRight();
        }

        if (command == "Left")
        {
            player.MoveLeft();
            
        } 
        if (command == "Stop")
        {
            player.Stop();
        }
        if (command == "Jump")
        {
            player.Jump();
        }
       
        if (command == "Shoot")
        {
            player.Shoot();
        }
    }

    public void MenuControl(string btn)
    {
        sorce.PlayOneShot(click);
        if (btn == "StartOver")
        {
            SceneManager.LoadScene("MainLevel");
            Time.timeScale = 1;
        }

        if (btn == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }

        if (btn == "MainMenu")
        {
            SceneManager.LoadScene("OpenScene");
        }

        if (btn == "Quit")
        {
            Application.Quit();
        }

        if (btn == "About")
        {
            SceneManager.LoadScene("About");
        }

        if (btn == "Escape")
        {
            Time.timeScale = 0;
            escapePanel.gameObject.SetActive(true);
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                tutorialPanel.gameObject.SetActive(false);
            }
        }

        if (btn == "Continue")
        {
            Time.timeScale = 1;
            escapePanel.gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                tutorialPanel.gameObject.SetActive(true);
            }
        }
        if (btn == "Skip")
        {
            SceneManager.LoadScene("MainLevel");
        }

        if(btn == "AboutTheGame")
        {
            aboutTheGameText.gameObject.SetActive(true);
            // disabling other texts
            howToPlayText.gameObject.SetActive(false);
            aboutUsText.gameObject.SetActive(false);
        }

        if (btn == "HowToPlay")
        {
            howToPlayText.gameObject.SetActive(true);
            // disabling other texts
            aboutTheGameText.gameObject.SetActive(false);
            aboutUsText.gameObject.SetActive(false);
        }

        if (btn == "AboutUs")
        {
            aboutUsText.gameObject.SetActive(true);
            // disabling other texts
            aboutTheGameText.gameObject.SetActive(false);
            howToPlayText.gameObject.SetActive(false);
        }
    }
}
