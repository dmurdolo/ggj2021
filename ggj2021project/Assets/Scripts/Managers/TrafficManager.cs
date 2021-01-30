using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public GameObject[] Vehicles;

    private GameObject VehiclesParent;

    void Start()
    {
    }

    public void Init()
    {
        VehiclesParent = GameObject.Find("Vehicles");

        PlaceVehicle(1, 1, -90);
    }

    void Update()
    {
    }

    private void PlaceVehicle(int x, int y, int direction)
    {
        Vector2 worldPosition = WorldManager.GetTileWorldPosition(x, y);
        GameObject vehicle = Instantiate(Vehicles[Random.Range(0, Vehicles.Length)]);
        vehicle.transform.parent = VehiclesParent.transform;
        vehicle.transform.eulerAngles = new Vector3(0, direction, 0);
        vehicle.transform.position = new Vector3(worldPosition.x, 0.005f, worldPosition.y);
    }
}
