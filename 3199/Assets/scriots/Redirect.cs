using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Redirect : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("BaseScene");
    }

    public void PlayMulti()
    {
        SceneManager.LoadScene("Loading");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
