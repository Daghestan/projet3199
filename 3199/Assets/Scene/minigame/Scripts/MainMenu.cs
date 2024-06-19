using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void nextBre()
    {
        SceneManager.LoadScene("explibretagne");
    }
    public void nextMaroc()
    {
        SceneManager.LoadScene("explimaroc");
    }
    public void nextouzb()
    {
        SceneManager.LoadScene("expliouzb");
    }
    public void nextTahiti()
    {
        SceneManager.LoadScene("explitiki");
    }

    public void GoToquete()
    {
        SceneManager.LoadScene("quete");

    }
    public void GoToGameMemory()
    {
        SceneManager.LoadScene("memorygame");

    }
    public void expliQuete()
    {
        SceneManager.LoadScene("expliQuete");

    }

    public void FinMeme()
    {
        SceneManager.LoadScene("EndMem");

    }
    public void GoToMainScene()
    {
        SceneManager.LoadScene ("Game");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene ("Menu");
    }
     public void Quit()
     {
         Application.Quit();
     }




}