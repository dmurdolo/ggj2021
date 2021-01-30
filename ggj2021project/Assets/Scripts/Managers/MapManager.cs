﻿using UnityEngine;

public class MapManager : MonoBehaviour
{
    public bool GenerateMap = true;
    public int Level = 1;

    public TextAsset[] levelMaps;

    public GameObject DefaultTile;
    public GameObject IslandTile;
    public GameObject CheckpointTile;

    public GameObject[] CurbTiles;
    public GameObject[] RoadTiles;
    public GameObject[] BuildingTiles;

    private GameObject MapParent;
    private string mapString;
    private float IslandOffset = 0;

    void Start()
    {
    }

    public void Init()
    {
        MapParent = GameObject.Find("Map");

        if (Level == 0)
        {
            WorldManager.MapSize = new Vector2Int(40, 40);
            if (GenerateMap)
            {
                CreateDefaultTown();
            }
        }
        else if (Level == 1)
        {
            WorldManager.MapSize = new Vector2Int(40, 40);
            TextAsset levelText = levelMaps[0];

            if (levelText)
            {
                // Dan's and Bens's computers are behaving differently
                // Applying both here to be safe
                mapString = levelText.text.Replace("\r\n", "");   // Ben
                mapString = mapString.Replace("\n", "");     // Dan - TODO Test this
            }
            else
            {
                Debug.LogError("Couldn't load level!");
            }
            
            if (GenerateMap)
            {
                CreateTown();
            }
        }
    }

    public Vector2Int GetRandomRoadTile()
    {
        int index;
        do
        {
            index = Random.Range(0, mapString.Length);
        } while (mapString[index] != 'R');
        
        int x = index % WorldManager.MapSize.x;
        int y = Mathf.FloorToInt(index / WorldManager.MapSize.y);

        return new Vector2Int(x, y);
    }

    private void CreateDefaultTown()
    {
        for (int y = 0; y < WorldManager.MapSize.y; y++)
        {
            for (int x = 0; x < WorldManager.MapSize.x; x++)
            {
                WorldManager.CreateTile(MapParent, DefaultTile, x, y);
            }
        }
    }

    private void CreateTown()
    {
        // 0 0 0
        // 0 R 0
        // 0 0 0

        for(int y = 0; y < WorldManager.MapSize.y; y++)
        {
            for (int x = 0; x < WorldManager.MapSize.x; x++)
            {
                string mapCharacter = GetMapCharacterAt(mapString, x, y);

                // Road
                if (mapCharacter == "R")
                {
                    string grid = GetRoadGrid(x, y);

                    //Straight Up
                    if (grid == "1010") WorldManager.CreateTile(MapParent, RoadTiles[Random.Range(0, 2)], x, y);
                    // Straight Across
                    else if (grid == "0101") WorldManager.CreateTile(MapParent, RoadTiles[Random.Range(3, 5)], x, y);
                    // Cross
                    else if (grid == "1111") WorldManager.CreateTile(MapParent, RoadTiles[6], x, y); // 2 crosses
                    // T right
                    else if (grid == "1110") WorldManager.CreateTile(MapParent, RoadTiles[7], x, y);
                    // T left
                    else if (grid == "1011") WorldManager.CreateTile(MapParent, RoadTiles[8], x, y);
                    // T down
                    else if (grid == "0111") WorldManager.CreateTile(MapParent, RoadTiles[9], x, y);
                    // T up
                    else if (grid == "1101") WorldManager.CreateTile(MapParent, RoadTiles[10], x, y);
                    // Corner Down Right
                    else if (grid == "0110") WorldManager.CreateTile(MapParent, RoadTiles[11], x, y);
                    // Corner Down Left
                    else if (grid == "0011") WorldManager.CreateTile(MapParent, RoadTiles[12], x, y);
                    // Corner up Right
                    else if (grid == "1100") WorldManager.CreateTile(MapParent, RoadTiles[13], x, y);
                    // Corner Up Left
                    else if (grid == "1001") WorldManager.CreateTile(MapParent, RoadTiles[14], x, y);
                    else WorldManager.CreateTile(MapParent, RoadTiles[Random.Range(0, 2)], x, y);
                }

                // Curb
                else if (mapCharacter == "C")
                {
                    string grid = GetRoadGrid(x, y);

                    // NESW
                    if (grid == "0100") WorldManager.CreateTile(MapParent, CurbTiles[0], x, y);
                    else if (grid == "0010") WorldManager.CreateTile(MapParent, CurbTiles[1], x, y);
                    else if (grid == "0001") WorldManager.CreateTile(MapParent, CurbTiles[2], x, y);
                    else if (grid == "1000") WorldManager.CreateTile(MapParent, CurbTiles[3], x, y);
                }

                // Island
                else if (mapCharacter == "I")
                {
                    GameObject island = WorldManager.CreateTile(MapParent, IslandTile, x, y);
                    island.GetComponent<Island>().Offset = IslandOffset;
                    IslandOffset += 1.0f;
                }

                // Building
                else if (mapCharacter == "B")
                {
                    WorldManager.CreateTile(MapParent, DefaultTile, x, y);
                    WorldManager.CreateTile(MapParent, BuildingTiles[Random.Range(0, BuildingTiles.Length)], x, y);
                }

                // Checkpoint
                else if (mapCharacter == "P")
                {
                    GameObject checkpoint = WorldManager.CreateTile(MapParent, CheckpointTile, x, y);
                }

                // Grass
                else
                {
                    WorldManager.CreateTile(MapParent, DefaultTile, x, y);
                }
            }
        }
    }

    private string GetMapCharacterAt(string map, int x, int y)
    {
        return map.Substring(x + (y * WorldManager.MapSize.x), 1);
    }

    // Returns a string of the NESW values, e.g. 0000, 0100, etc.
    public string GetRoadGrid(int x, int y)
    {
        string grid = "";

        // --------------------------------------------------------------------

        // Top left
        if (x == 0 || y == 0) grid += "0";
        else grid += IsRoad(mapString, x - 1, y - 1) ? "1" : "0";

        // Top middle
        if (y == 0) grid += "0";
        else grid += IsRoad(mapString, x, y - 1) ? "1" : "0";

        // Top right
        if (x == WorldManager.MapSize.x - 1 || y == 0) grid += "0";
        else grid += IsRoad(mapString, x + 1, y - 1) ? "1" : "0";

        // --------------------------------------------------------------------

        // Left
        if (x == 0) grid += "0";
        else grid += IsRoad(mapString, x - 1, y) ? "1" : "0";

        grid += "1";

        // Right
        if (x == WorldManager.MapSize.x - 1) grid += "0";
        else grid += IsRoad(mapString, x + 1, y) ? "1" : "0";

        // --------------------------------------------------------------------

        // Bottom left
        if (x == 0 || y == WorldManager.MapSize.y - 1) grid += "0";
        else grid += IsRoad(mapString, x - 1, y + 1) ? "1" : "0";

        // Bottom middle
        if (y == WorldManager.MapSize.y - 1) grid += "0";
        else grid += IsRoad(mapString, x, y + 1) ? "1" : "0";

        // Bottom right
        if (x == WorldManager.MapSize.x - 1 || y == WorldManager.MapSize.y - 1) grid += "0";
        else grid += IsRoad(mapString, x + 1, y + 1) ? "1" : "0";

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
        return charAt == "R";
    }
}
