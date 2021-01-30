using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static Vector2Int MapSize = new Vector2Int(100, 100);
    public static Vector2Int TileSize = new Vector2Int(1, 1);

    void Start()
    {
        GetComponent<MapManager>().Init();
        GetComponent<TrafficManager>().Init();
    }

    public static Vector2 GetTilePosition(int x, int y)
    {
        Vector2 offset = new Vector2(((MapSize.x - 1) * TileSize.x) / 2.0f, ((MapSize.y - 1) * TileSize.y) / 2.0f);
        return new Vector2(-x * TileSize.x + offset.x, y * TileSize.y - offset.y);
    }

    public static void CreateTile(GameObject parent, GameObject go, int x, int y)
    {
        Vector2 offset = new Vector2(((MapSize.x - 1) * TileSize.x) / 2.0f, ((MapSize.y - 1) * TileSize.y) / 2.0f);

        GameObject tile = Instantiate(go);
        tile.transform.parent = parent.transform;
        tile.transform.position = new Vector3(-x * TileSize.x + offset.x, 0, y * TileSize.y - offset.y);
    }
}
