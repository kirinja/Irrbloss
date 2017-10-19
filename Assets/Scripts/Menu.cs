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
}
