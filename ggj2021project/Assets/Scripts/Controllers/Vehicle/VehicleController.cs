using UnityEngine;
using UnityEngine.AI;

public class VehicleController : MonoBehaviour
{
    [TextArea]
    public string DebugText;

    private MapManager mapManager;
    private NavMeshAgent agent;
    private Vector2Int currentTileDestination = Vector2Int.zero;
    private Vector3 currentWorldDestination = Vector3.zero;

    void Start()
    {
        mapManager = GameObject.Find("Main").GetComponent<MapManager>();
        agent = GetComponent<NavMeshAgent>();

        SetNewDestination();
    }

    void Update()
    {
        agent.SetDestination(currentWorldDestination);

        if (HasReachedDestination())
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        currentTileDestination = mapManager.GetRandomRoadTile();
        currentWorldDestination = WorldManager.GetTileWorldPosition(currentTileDestination);
        
        DebugText = "Destination: " + currentTileDestination;
    }

    private bool HasReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                   return true;
                }
            }
        }

        return false;
    }
}
