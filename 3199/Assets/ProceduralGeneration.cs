using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] roomTiles; // Tableau contenant vos tiles de salle
    public int mapWidth = 10; // Largeur de la carte
    public int mapHeight = 10; // Hauteur de la carte
    private float blocSep = 1.5f; // Séparation entre chaque bloc

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float xPos = (x - y) * blocSep / 2f;
                float yPos = (x + y) * blocSep / 2f;
                Vector3Int cellPosition = new Vector3Int(Mathf.FloorToInt(xPos), Mathf.FloorToInt(yPos), 0);

                int randomRoomIndex = Random.Range(0, roomTiles.Length);
                tilemap.SetTile(cellPosition, roomTiles[randomRoomIndex]);
            }
        }
    }
}
