using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float Speed = 1.0f;

    private bool isFirstChange = true;
    private bool isStationary = true;
    private Vector2Int lastTilePosition = new Vector2Int(-1, -1);

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        // Get tile position
        Vector2Int tilePosition = WorldManager.GetTilePosition(transform.position);
        Vector2 tilePositionExact = WorldManager.GetTilePositionExact(transform.position);
        WorldManager.SetDebugText("" + tilePosition.x + ", " + tilePosition.y);

        bool isPastHalfway = true;
        WorldManager.Direction direction = WorldManager.Direction.Up;
        if (transform.eulerAngles.y == 180)   // Up
        {
            direction = WorldManager.Direction.Up;
            isPastHalfway = ((tilePositionExact.y - tilePosition.y) <= 0.5f);
        }
        else if (transform.eulerAngles.y == 0)   // Down
        {
            direction = WorldManager.Direction.Down;
            isPastHalfway = ((tilePositionExact.y - tilePosition.y) >= 0.5f);
        }
        else if (transform.eulerAngles.y == 90)   // Left
        {
            direction = WorldManager.Direction.Left;
            isPastHalfway = ((tilePositionExact.x - tilePosition.x) <= 0.5f);
        }
        else if (transform.eulerAngles.y == 270)   // Right
        {
            direction = WorldManager.Direction.Right;
            isPastHalfway = ((tilePositionExact.x - tilePosition.x) >= 0.5f);
        }

        // Change direction
        if (tilePosition != lastTilePosition && isPastHalfway)
        {
            string roadGrid = WorldManager.GetRoadGrid(tilePosition);
            lastTilePosition = WorldManager.GetTilePosition(transform.position);

            // T right
            if (roadGrid == "1110")
            {
                if (direction == WorldManager.Direction.Up || direction == WorldManager.Direction.Down)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 270, 0);   // Right
                    }
                }
                else if (direction == WorldManager.Direction.Left)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);   // Up
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);   // Down
                    }
                }
            }
            // T left
            else if (roadGrid == "1011")
            {
                if (direction == WorldManager.Direction.Up || direction == WorldManager.Direction.Down)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 90, 0);   // Left
                    }
                }
                else if (direction == WorldManager.Direction.Right)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);   // Up
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);   // Down
                    }
                }
            }
            // T down
            else if (roadGrid == "0111")
            {
                if (direction == WorldManager.Direction.Right || direction == WorldManager.Direction.Left)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);   // Down
                    }
                }
                else if (direction == WorldManager.Direction.Up)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 90, 0);   // Left
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 270, 0);   // Right
                    }
                }
            }
            // T up
            else if (roadGrid == "1101")
            {
                if (direction == WorldManager.Direction.Right || direction == WorldManager.Direction.Left)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);   // Up
                    }
                }
                else if (direction == WorldManager.Direction.Down)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        transform.eulerAngles = new Vector3(0, 90, 0);   // Left
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 270, 0);   // Right
                    }
                }
            }

            // Cross
            else if (roadGrid == "1111") Debug.Log("Cross");

            // Straight Up
            else if (roadGrid == "1010") Debug.Log("Straight Up");

            // Straight Across
            else if (roadGrid == "0101") Debug.Log("Straight Down");

            // Corner Down Right
            else if (roadGrid == "0110")
            {
                if (direction == WorldManager.Direction.Left)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);   // Down
                }
                else if (direction == WorldManager.Direction.Up)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);   // Right
                }
            }
            // Corner Down Left
            else if (roadGrid == "0011")
            {
                if (direction == WorldManager.Direction.Right)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);   // Down
                }
                else if (direction == WorldManager.Direction.Up)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);   // Left
                }
            }
            // Corner Up Right
            else if (roadGrid == "1100")
            {
                if (direction == WorldManager.Direction.Down)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);   // Right
                }
                else if (direction == WorldManager.Direction.Left)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);   // Up
                }
            }
            // Corner Up Left
            else if (roadGrid == "1001")
            {
                if (direction == WorldManager.Direction.Right)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);   // Up
                }
                else if (direction == WorldManager.Direction.Down)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);   // Left
                }
            }
        }
    }
}
