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

    [SerializeField]
    private bool keepMoving = true;
    [SerializeField]
    private bool notAtCheckpoint = true;
    [SerializeField]
    private bool firstCheckpoint = true;

    TrailRenderer trail;
    GameObject[] wheels;
    MeshRenderer car;

    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GameObject.Find("Managers").GetComponent<CheckpointManager>();
        agent = GetComponent<NavMeshAgent>();

        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        wheels = GameObject.FindGameObjectsWithTag("Wheel");
        car = transform.GetComponent<MeshRenderer>();

        StartCoroutine(SetNewDestination());
        StartCoroutine(RandomlyDisappear());
    }

    // Update is called once per frame
    void Update()
    {
        if (HasReachedDestination())
        {
            TrailRenderer trail = transform.Find("Trail").GetComponent<TrailRenderer>();
            GameObject[] wheels = GameObject.FindGameObjectsWithTag("Wheel");
            MeshRenderer car = transform.GetComponent<MeshRenderer>();

            firstCheckpoint = false;
            notAtCheckpoint = false;

            car.enabled = true;
            ToggleWheels(wheels, true);
        }

        if (checkpointManager.GetCurrentCheckpoint() >= checkpointManager.Checkpoints.Length)
        {
            StopAllCoroutines();
        }
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

    IEnumerator SetNewDestination()
    {
        while (keepMoving)
        {
            yield return new WaitForSeconds(firstCheckpoint ? 0f : 4f);

            notAtCheckpoint = true;

            currentWorldDestination = checkpointManager.Checkpoints.Length > 0 ? checkpointManager.GetCurrentCheckpointPosition() : transform.position;
            agent.SetDestination(currentWorldDestination);

            DebugText = "Destination: " + currentWorldDestination;
        }
    }

    IEnumerator RandomlyDisappear()
    { 
        while (notAtCheckpoint)
        {   
            Debug.Log(notAtCheckpoint);

            int rnd1 = Random.Range(2, 6);

            yield return new WaitForSeconds(rnd1);

            ToggleWheels(wheels, false);
            car.enabled = false;
            trail.emitting = true;

            int rnd2 = Random.Range(2, 6);

            yield return new WaitForSeconds(rnd2);

            ToggleWheels(wheels, true);
            car.enabled = true;
            trail.emitting = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider theirs = collision.gameObject.GetComponent<Collider>();
        Collider mine = GetComponent<Collider>();

        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(theirs, mine);
        }

        if (collision.gameObject.tag == "Vehicle")
        {
            Physics.IgnoreCollision(theirs, mine);
        }

        if (collision.gameObject.tag == "Wheel")
        {
            Physics.IgnoreCollision(theirs, mine);
        }
    }
}
