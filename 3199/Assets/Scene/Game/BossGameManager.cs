using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Completed; 
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class BossGameManager : MonoBehaviour
{
    public static BossGameManager instance = null;
    private BossBoardManager boardScript;
    public int level = 1;

    void Awake()
    {

        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);    

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BossBoardManager>();
 
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


    void Update()
    {

    }
}

