using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostCarController : MonoBehaviour
{
    [TextArea]
    public string DebugText;

    private CheckpointManager checkpointManager;
    private NavMeshAgent agent;
    private Vector2Int currentTileDestination = Vector2Int.zero;
    private Vector3 currentWorldDestination = Vector3.zero;

    private bool goneToNextScenario = false;

    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GameObject.Find("Managers").GetComponent<CheckpointManager>();
        agent = GetComponent<NavMeshAgent>();

        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasReachedDestination())
        {
            TrailRenderer trail = transform.Find("Trail").GetComponent<TrailRenderer>();
            GameObject[] wheels = GameObject.FindGameObjectsWithTag("Wheel");
            MeshRenderer car = transform.GetComponent<MeshRenderer>();

            StopCoroutine(RandomlyDisappearCoroutine());

            car.enabled = true;
            ToggleWheels(wheels, true);
            trail.emitting = false;

            SetNewDestination();
            //StartCoroutine(GoToNextScenario());
        }
    }

    private void SetNewDestination()
    {
        currentWorldDestination = checkpointManager.GetCurrentCheckpointPosition();
        agent.SetDestination(currentWorldDestination);

        //StopCoroutine(GoToNextScenario());
        StartCoroutine(RandomlyDisappearCoroutine());

        DebugText = "Destination: " + currentWorldDestination;
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
    void ToggleWheels(GameObject[] wheels, bool on)
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.GetComponent<MeshRenderer>().enabled = on;
        }
    }

    IEnumerator RandomlyDisappearCoroutine()
    {
        TrailRenderer trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        GameObject[] wheels = GameObject.FindGameObjectsWithTag("Wheel");
        MeshRenderer car = transform.GetComponent<MeshRenderer>();

        while (true)
        {
            int rnd1 = Random.Range(0, 5);

            yield return new WaitForSeconds(rnd1);

            ToggleWheels(wheels, false);
            car.enabled = false;
            trail.emitting = true;

            int rnd2 = Random.Range(0, 5);

            yield return new WaitForSeconds(rnd2);

            ToggleWheels(wheels, true);
            car.enabled = true;
            trail.emitting = false;
        }
    }

    IEnumerator GoToNextScenario()
    {
        if (goneToNextScenario)
            yield break;

        goneToNextScenario = true;

        yield return new WaitForSeconds(10f);
        SetNewDestination();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}
