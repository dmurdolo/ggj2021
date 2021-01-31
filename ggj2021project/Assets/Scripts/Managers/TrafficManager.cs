using UnityEngine;
using UnityEngine.AI;

public class TrafficManager : MonoBehaviour
{
    // Assume left hand side of the road

    public GameObject[] Vehicles;
    public int NumVehiclesToSpawn = 4;

    private MapManager mapManager;
    private GameObject VehiclesParent;

    void Start()
    {
    }

    public void Init()
    {
        mapManager = GetComponent<MapManager>();

        VehiclesParent = GameObject.Find("Vehicles");

        for (int i = 0; i < NumVehiclesToSpawn; i++)
        {
            PlaceVehicle(mapManager.GetRandomRoadTile(), (WorldManager.Direction)Random.Range(0, 3));
        }

        //PlaceVehicle(4, 4, WorldManager.Direction.Right);
    }

    private void PlaceVehicle(Vector2Int tilePosition, WorldManager.Direction direction)
    {
        PlaceVehicle(tilePosition.x, tilePosition.y, direction);
    }

    private void PlaceVehicle(int x, int y, WorldManager.Direction direction)
    {
        int yAngle = 0;
        float xOffset = 0;   // Adjust for left side of the road
        float yOffset = 0;

        switch(direction)
        {
            case WorldManager.Direction.Up:
                yAngle = 180;
                break;
            
            case WorldManager.Direction.Down:
                yAngle = 0;
                break;
            
            case WorldManager.Direction.Left:
                yAngle = 90;
                break;
            
            case WorldManager.Direction.Right:
                yAngle = 270;
                break;
        }

        Vector2 worldPosition = WorldManager.GetTileWorldXY(x, y);
        GameObject vehicle = Instantiate(Vehicles[Random.Range(0, Vehicles.Length)]);
        vehicle.GetComponent<NavMeshAgent>().enabled = false;   // It gets placed in the wrong position if you don't do this
        vehicle.transform.parent = VehiclesParent.transform;
        vehicle.transform.eulerAngles = new Vector3(0, yAngle, 0);
        vehicle.transform.position = new Vector3(worldPosition.x + xOffset, 0.005f, worldPosition.y + yOffset);
        vehicle.GetComponent<NavMeshAgent>().enabled = true;
    }
}
