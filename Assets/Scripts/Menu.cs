using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    

    public void StartGame()
    {
        SceneManager.LoadScene("FinishedLevel");
    }

    public void LoadInstruction()
    {
        SceneManager.LoadScene("Instruktion");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadSubDoor()
    {
        SceneManager.LoadScene("SubDoor");
    }

    public void LoadSubEnemy()
    {
        SceneManager.LoadScene("SubEnemy");
    }

    public void LoadSubLight()
    {
        SceneManager.LoadScene("SubLight");
    }
}
