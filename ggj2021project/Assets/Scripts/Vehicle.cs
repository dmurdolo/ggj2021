using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float Speed = 0.5f;

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
        WorldManager.SetDebugText("" + tilePosition.x + ", " + tilePosition.y + " " + (tilePositionExact.x - tilePosition.x));

        bool isPastHalfway = true;
        if (transform.eulerAngles.y == 180)   // Up
        {
            isPastHalfway = ((tilePositionExact.y - tilePosition.y) <= 0.5f);
        }
        else if (transform.eulerAngles.y == 0)   // Down
        {
            isPastHalfway = ((tilePositionExact.y - tilePosition.y) >= 0.5f);
        }
        else if (transform.eulerAngles.y == 270)   // Right
        {
            isPastHalfway = ((tilePositionExact.x - tilePosition.x) >= 0.5f);
        }
        else if (transform.eulerAngles.y == 90)   // Left
        {
            isPastHalfway = ((tilePositionExact.x - tilePosition.x) <= 0.5f);
        }

        // Change direction
        if (tilePosition != lastTilePosition && isPastHalfway)
        {
            string roadGrid = WorldManager.GetRoadGrid(tilePosition);
            //WorldManager.SetDebugText("" + tilePosition.x + ", " + tilePosition.y + " - " + roadGrid);
            lastTilePosition = WorldManager.GetTilePosition(transform.position);

            // T right
            if (roadGrid == "1110")
            {
                // Test
                transform.eulerAngles = new Vector3(0, 180, 0);   // Up
            }
            // T left
            else if (roadGrid == "1011") Debug.Log("");
            
            // T down
            else if (roadGrid == "0111")
            {
                if (Random.Range(0, 3) == 0)
                {
                }

                // Test
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            // T up
            else if (roadGrid == "1101") Debug.Log("");

            // Cross
            else if (roadGrid == "1111") Debug.Log("");

            // Straight Up
            else if (roadGrid == "1010") Debug.Log("");
            // Straight Across
            else if (roadGrid == "0101") Debug.Log("");

            // Corner Down Right
            else if (roadGrid == "0110")
            {
                // Randomly choose right or down
                if (Random.Range(0, 2) == 0)
                {
                    transform.eulerAngles = new Vector3(0, -90, 0);   // Right
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);   // Down
                }

                // Test
                transform.eulerAngles = new Vector3(0, -90, 0);   // Right
            }
            // Corner Down Left
            else if (roadGrid == "0011") Debug.Log("");
            // Corner Up Right
            else if (roadGrid == "1100") Debug.Log("");
            // Corner Up Left
            else if (roadGrid == "1001")
            {
                // Test
                transform.eulerAngles = new Vector3(0, 90, 0);   // Left
            }
        }
    }
}
