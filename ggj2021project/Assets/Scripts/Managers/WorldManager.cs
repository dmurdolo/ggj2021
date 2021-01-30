using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }

    public static Vector2Int MapSize = new Vector2Int(100, 100);
    public static Vector2Int TileSize = new Vector2Int(1, 1);
    public static GameObject DebugText;

    private static MapManager MapManager;
    private static TrafficManager TrafficManager;

    void Start()
    {
        DebugText = GameObject.Find("DebugText");

        MapManager = GetComponent<MapManager>();
        MapManager.Init();

        TrafficManager = GetComponent<TrafficManager>();
        TrafficManager.Init();
    }

    void Update()
    {
        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject vehiclesParent = GameObject.Find("Vehicles");
            foreach (Transform child in vehiclesParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            TrafficManager.Init();
        }
    }

    public static void SetDebugText(string debugText)
    {
        DebugText.GetComponent<Text>().text = debugText;
    }

    public static Vector2 GetOffset()
    {
        return new Vector2(((MapSize.x - 1) * TileSize.x) / 2.0f, ((MapSize.y - 1) * TileSize.y) / 2.0f);
    }

    // Get the world position of the specified tile
    public static Vector2 GetTileWorldPosition(int x, int y)
    {
        Vector2 offset = GetOffset();
        return new Vector2(-x * TileSize.x + offset.x, y * TileSize.y - offset.y);
    }

    // Get the tile position from the specified world position
    public static Vector2Int GetTilePosition(Vector3 worldPosition)
    {
        Vector2 offset = GetOffset();
        return new Vector2Int(
            Mathf.FloorToInt(-(worldPosition.x / TileSize.x - (TileSize.x / 2.0f)) + offset.x),
            Mathf.FloorToInt((worldPosition.z / TileSize.y + (TileSize.y / 2.0f)) + offset.y));
    }

    public static Vector2 GetTilePositionExact(Vector3 worldPosition)
    {
        Vector2 offset = GetOffset();
        return new Vector2(
            -(worldPosition.x / TileSize.x - (TileSize.x / 2.0f)) + offset.x,
            (worldPosition.z / TileSize.y + (TileSize.y / 2.0f)) + offset.y);
    }

    public static void CreateTile(GameObject parent, GameObject go, int x, int y)
    {
        Vector2 offset = new Vector2(((MapSize.x - 1) * TileSize.x) / 2.0f, ((MapSize.y - 1) * TileSize.y) / 2.0f);

        GameObject tile = Instantiate(go);
        tile.transform.parent = parent.transform;
        tile.transform.position = new Vector3(-x * TileSize.x + offset.x, 0, y * TileSize.y - offset.y);
    }

    public static string GetRoadGrid(Vector2Int tilePosition)
    {
        return MapManager.GetRoadGrid(tilePosition.x, tilePosition.y);
    }
}
