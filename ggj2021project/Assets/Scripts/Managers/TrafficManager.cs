using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    // Assume left hand side of the road

    public GameObject[] Vehicles;

    private GameObject VehiclesParent;

    void Start()
    {
    }

    public void Init()
    {
        VehiclesParent = GameObject.Find("Vehicles");

        PlaceVehicle(1, 2, WorldManager.Direction.Right);
        PlaceVehicle(23, 2, WorldManager.Direction.Down);
        PlaceVehicle(23, 23, WorldManager.Direction.Left);
        PlaceVehicle(1, 23, WorldManager.Direction.Up);
    }

    void Update()
    {
    }

    private void PlaceVehicle(int x, int y, WorldManager.Direction direction)
    {
        int yAngle = 0;
        float xOffset = 0;
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
                //yOffset = -0.055f;
                break;
        }

        Vector2 worldPosition = WorldManager.GetTileWorldPosition(x, y);
        GameObject vehicle = Instantiate(Vehicles[Random.Range(0, Vehicles.Length)]);
        vehicle.transform.parent = VehiclesParent.transform;
        vehicle.transform.eulerAngles = new Vector3(0, yAngle, 0);
        vehicle.transform.position = new Vector3(worldPosition.x + xOffset, 0.005f, worldPosition.y + yOffset);
    }
}
