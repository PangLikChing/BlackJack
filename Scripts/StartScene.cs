using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] GameObject warningMessageText, playerNameInputField, playButton;
    [SerializeField] Text playerNameInput;
    [SerializeField] Player player;
    [SerializeField] KeyCode enterKey;

    void Awake()
    {
        //test
        PlayerPrefs.SetString("PlayerName", "");
        PlayerPrefs.SetInt("PlayerCoins", 0);

        //I am lazy, so I use PlayerPrefs
        //If there is no PlayerName
        if (PlayerPrefs.GetString("PlayerName") == "")
        {
            //Let player setup
            playerNameInputField.SetActive(true);
            playButton.SetActive(true);
        }
        //If there is
        else
        {
            //Load Play Scene
            SceneManager.LoadScene(1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(enterKey))
        {
            CreatePlayer();
        }
    }

    public void CreatePlayer()
    {
        //Get name input from player
        if (playerNameInput.text == "")
        {
            //Display warning message
            warningMessageText.SetActive(true);
            Debug.Log("Error");
        }
        else
        {
            //Save it in database later
            PlayerPrefs.SetString("PlayerName", playerNameInput.text);
            PlayerPrefs.SetInt("PlayerCoins", 1000);
            //Player class to actually save the data for the game if there is a database
            player.playerName = playerNameInput.text;
            player.playerCoins = 1000;
            //Load Play Scene
            SceneManager.LoadScene(1);
        }
    }

    public void Quit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
