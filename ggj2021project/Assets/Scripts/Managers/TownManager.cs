using UnityEngine;

public class TownManager : MonoBehaviour
{
    public Vector2Int TileSize = new Vector2Int(1, 1);

    public int Level = 1;

    public GameObject MapParent;
    public GameObject GrassTile;

    public GameObject[] RoadTiles;
    public GameObject[] BuildingTiles;
    
    private Vector2Int TownSize = new Vector2Int(100, 100);

    void Start()
    {
        string map;

        if (Level == 0)
        {
            TownSize = new Vector2Int(25, 25);
            CreateGrassTown();
        }
        else if (Level == 1)
        {
            TownSize = new Vector2Int(25, 25);

            map =
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GRRRGGGGHHHGHGHGHGGGGGGGG" +
               "GRGRHGRRRRRRRRRRRRHGGGGGG" +
               "GRHRGHRHHRHHRHHRGRRGHGHGG" +
               "GRGRHGRGGRGGRGGRHGRRRRRRG" +

               "GRHRGHRHHRHHRHHRGGHRHGHRG" +
               "GRGRHGRGGRGGRGGRHGGRGGGRG" +
               "GRHRGHRHHRHHRHHRGGHRHGHRG" +
               "GRGRHGRGGRGGRGGRHGGRGGGRG" +
               "GRHRGHRHHRHHRHHRGGHRHGHRG" +

               "GRRRHGRGGRGGRGGRHGGRGGGRG" +
               "GRHRGGRHHRHHRHHRGGHRHGHRG" +
               "GRGRRRRRRRRRRRRRRRRRRRRRG" +
               "GRHRGGGGGRGGGGGRGGGGGGGRG" +
               "GRGRRRRRRRRRRRRRRRRRRRRRG" +

               "GRHRGHGRGHGRGGGRGGGRGGGRG" +
               "GRGRHGHRHGHRHGHRGGGRGHRRG" +
               "GRRRGGGRGGGRGGGRGGGRGGGRG" +
               "GRHRHGHRHGHRHGHRGGGRGHRRG" +
               "GRHRGGGRGGGRGGGRGGGRGGGRG" +

               "GRGRHGHRHGHRGHRRGGGRRGGRG" +
               "GRHRGGGRGGGRGRRGGGGGRRGRG" +
               "GRHRGHGRGHGRRRHGGGGGGRRRG" +
               "GRRRRRRRRRRRGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG";

            map =
               "GBBBBGGGGGGGGGGGGGGGGGGGG" +
               "GRRRRGGGGGGGGGGGGGGGGGGGG" +
               "GRGGRGGGGGGGGGGGGGGGGGGGG" +
               "GRGGRGGGGGGGGGGGGGGGGGGGG" +
               "GRRRRGGGGGGGGGGGGGGGGGGGG" +

               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +

               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +

               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +

               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG" +
               "GGGGGGGGGGGGGGGGGGGGGGGGG";
            
            CreateTown(map);
        }
    }

    private void CreateGrassTown()
    {
        for (int y = 0; y < TownSize.y; y++)
        {
            for (int x = 0; x < TownSize.x; x++)
            {
                CreateTile(GrassTile, x, y);
            }
        }
    }

    private void CreateTown(string map)
    {
        // 0 0 0
        // 0 R 0
        // 0 0 0

        for(int y = 0; y < TownSize.y; y++)
        {
            for (int x = 0; x < TownSize.x; x++)
            {
                string mapCharacter = GetMapCharacterAt(map, x, y);

                // Road
                if (mapCharacter == "R")
                {
                    string grid = GetRoadGrid(map, x, y);

                    // T right
                    if (grid == "1110") CreateTile(RoadTiles[0], x, y);
                    // T left
                    else if (grid == "1011") CreateTile(RoadTiles[1], x, y);
                    // T down
                    else if (grid == "0111") CreateTile(RoadTiles[2], x, y);
                    // T up
                    else if (grid == "1101") CreateTile(RoadTiles[3], x, y);

                    // Cross
                    else if (grid == "1111") CreateTile(RoadTiles[Random.Range(4, 5)], x, y);   // 2 crosses

                    // Straight Up
                    else if (grid == "1010") CreateTile(RoadTiles[6], x, y);
                    // Straight Across
                    else if (grid == "0101") CreateTile(RoadTiles[7], x, y);

                    // Corner Down Right
                    else if (grid == "0110") CreateTile(RoadTiles[8], x, y);
                    // Corner Down Left
                    else if (grid == "0011") CreateTile(RoadTiles[9], x, y);
                    // Corner Up Right
                    else if (grid == "1100") CreateTile(RoadTiles[10], x, y);
                    // Corner Up Left
                    else if (grid == "1001") CreateTile(RoadTiles[11], x, y);
                }
                
                // Building
                else if (mapCharacter == "B")
                {
                    CreateTile(GrassTile, x, y);
                    CreateTile(BuildingTiles[Random.Range(0, BuildingTiles.Length)], x, y);
                }

                // Grass
                else
                {
                    CreateTile(GrassTile, x, y);
                }
            }
        }
    }

    private string GetMapCharacterAt(string map, int x, int y)
    {
        return map.Substring(x + (y * TownSize.x), 1);
    }

    // Returns a string of the NSEW values, e.g. 0000, 0100, etc.
    private string GetRoadGrid(string map, int x, int y)
    {
        string grid = "";

        // --------------------------------------------------------------------

        // Top left
        if (x == 0 || y == 0) grid += "0";
        else grid += IsRoad(map, x - 1, y - 1) ? "1" : "0";

        // Top middle
        if (y == 0) grid += "0";
        else grid += IsRoad(map, x, y - 1) ? "1" : "0";

        // Top right
        if (x == TownSize.x - 1 || y == 0) grid += "0";
        else grid += IsRoad(map, x + 1, y - 1) ? "1" : "0";

        // --------------------------------------------------------------------

        // Left
        if (x == 0) grid += "0";
        else grid += IsRoad(map, x - 1, y) ? "1" : "0";

        grid += "1";

        // Right
        if (x == TownSize.x - 1) grid += "0";
        else grid += IsRoad(map, x + 1, y) ? "1" : "0";

        // --------------------------------------------------------------------

        // Bottom left
        if (x == 0 || y == TownSize.y - 1) grid += "0";
        else grid += IsRoad(map, x - 1, y + 1) ? "1" : "0";

        // Bottom middle
        if (y == TownSize.y - 1) grid += "0";
        else grid += IsRoad(map, x, y + 1) ? "1" : "0";

        // Bottom right
        if (x == TownSize.x - 1 || y == TownSize.y - 1) grid += "0";
        else grid += IsRoad(map, x + 1, y + 1) ? "1" : "0";

        // --------------------------------------------------------------------

        // Just return NESW values - e.g. 0000
        // 0 1 2
        // 3 4 5
        // 6 7 8
        grid = grid.Substring(1, 1) + grid.Substring(5, 1) + grid.Substring(7, 1) + grid.Substring(3, 1);

        return grid;
    }

    // Check if there is a road at (x, y)
    private bool IsRoad(string map, int x, int y)
    {
        string charAt = GetMapCharacterAt(map, x, y);
        return charAt == "R" || charAt == "H";
    }

    private void CreateTile(GameObject go, int x, int y)
    {
        Vector2 offset = new Vector2(((TownSize.x - 1) * TileSize.x) / 2.0f, ((TownSize.y - 1) * TileSize.y) / 2.0f);

        GameObject tile = Instantiate(go);
        tile.transform.parent = MapParent.transform;
        tile.transform.position = new Vector3(-x * TileSize.x + offset.x, 0, y * TileSize.y - offset.y);
    }
}
