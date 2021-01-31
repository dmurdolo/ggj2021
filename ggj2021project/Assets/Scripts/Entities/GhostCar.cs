using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCar : MonoBehaviour
{
    public float Speed = 1.0f;

    private bool isFirstChange = true;
    private bool isStationary = true;
    private Vector2Int lastTilePosition = new Vector2Int(-1, -1);

    private WheelCollider[] wheels;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
