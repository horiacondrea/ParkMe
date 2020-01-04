using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void RandomButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void CampaignButton()
    {
        SceneManager.LoadScene("CampaignMenu");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        if (SceneManager.GetActiveScene().name == "CampaignMenu")
        {
            SceneManager.LoadScene("StartMenu");
        }
        else if(SceneManager.GetActiveScene().name == "StartMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }


    public void Level_0()
    {
        SceneManager.LoadScene("Level_0");
    }

    public void Level_1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void Level_4()
    {
        SceneManager.LoadScene("Level_4");
    }
}
