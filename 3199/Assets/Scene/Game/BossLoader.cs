using UnityEngine;
using System.Collections;


    public class BossLoader : MonoBehaviour 
    {
        public GameObject BossgameManager;

        

        void Awake ()
        {
            if (BossGameManager.instance == null)

                //Instantiate gameManager prefab
                Instantiate(BossgameManager);

        }
    }