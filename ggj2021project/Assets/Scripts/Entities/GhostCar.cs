using UnityEngine;

public class GhostCar : MonoBehaviour
{
    public float Speed = 1.0f;

    private Vector2Int lastTilePosition = new Vector2Int(-1, -1);
    private WheelCollider[] wheels;

    void Start()
    {
    }

    void Update()
    {
    }
}
