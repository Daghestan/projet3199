using UnityEngine;
using System;
//Allows us to use Lists.
using System.Collections.Generic;
//Tells Random to use the Unity Engine random number generator.
using Random = UnityEngine.Random;

namespace Completed

{

    public class BossBoardManager : MonoBehaviour
    {
        [Serializable]
        public class Count
        {
            public int minimum;             
            public int maximum;            

            
            public Count (int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns = 16;            
        public int rows = 16;                
        public Count wallCount = new Count (5, 12);
        public GameObject[] enemy;
        public GameObject[] floorTiles;     
        public GameObject[] wallTiles;      
        public GameObject[] outerWallTiles; 
        
        private Transform boardHolder;
        private List <Vector3> gridPositions = new List <Vector3> ();

        
        void InitialiseList ()
        {
            gridPositions.Clear ();
            
            for(int x = -199; x < columns - 201; x++)
            {
                for(int y = -199; y < rows -201; y++)
                {
                    gridPositions.Add (new Vector3(x, y, 0f));
                }
            }
        }

        
        void BoardSetup ()
        {
            boardHolder = new GameObject ("Board").transform;
            
            for(int x = -201; x < columns -199; x++)
            {
                for(int y = -201; y < rows + -199; y++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
                    
                    if(x == -201 || x == columns - 200 || y == -201 || y == rows - 200)
                        toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
                    
                    GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
                    
                    instance.transform.SetParent (boardHolder);
                }
            }
        }



        Vector3 RandomPosition ()
        {
            int randomIndex = Random.Range (0, gridPositions.Count);
            
            Vector3 randomPosition = gridPositions[randomIndex];
            
            gridPositions.RemoveAt (randomIndex);
            
            return randomPosition;
        }

        
        void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range (minimum, maximum+1);
            
            for(int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                
                GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
                
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }

        void spawnEnemy(GameObject[] enemy, int level)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject EnemyChoice = enemy[Random.Range (0, enemy.Length)];

            Instantiate(EnemyChoice, randomPosition, Quaternion.identity);
            
        }
        
        public void SetupScene (int level)
        {
            BoardSetup ();
            
            InitialiseList ();

            LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);

            spawnEnemy (enemy, level);

        }
    }
}