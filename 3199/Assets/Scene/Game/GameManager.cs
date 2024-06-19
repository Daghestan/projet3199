using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Completed; 
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private BoardManager boardScript;
    private int level = 1;
    public int kill = 0;

    void Awake()
    {

        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);    

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
 
        InitGame();
    }


    private void OnLevelWasLoaded (int index)
    {
        level++; 

        
        InitGame();

    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        
    }

    public void Kill()
    {
        kill += 1;
        if (kill == 15)
        {
            SceneManager.LoadScene("Boss");
        }
    }

    void Update()
    {

    }
}

