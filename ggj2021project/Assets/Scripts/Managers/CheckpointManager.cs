using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject[] Checkpoints;

    [SerializeField]
    private int _currentCheckpoint = 0;

    private DestinationDisplayManager _destinationDisplayManager;
    private NarrativeManager _narrativeManager;
    private CheckpointCounterManager _checkpointCounterManager;

    void Start()
    {
        // Destination display
        _destinationDisplayManager = GetComponent<DestinationDisplayManager>();
        if (!_destinationDisplayManager)
        {
            Debug.LogError("DestinationDisplayManager component not found.");
        }

        // Narrative
        _narrativeManager = GetComponent<NarrativeManager>();
        if (!_narrativeManager)
        {
            Debug.LogError("NarrativeManager component not found.");
        }

        // Checkpoint counter
        _checkpointCounterManager = GetComponent<CheckpointCounterManager>();
        if (!_checkpointCounterManager)
        {
            Debug.LogError("CheckpointCounterManager component not found.");
        }

        // Checkpoint gameobjects
        if (GameObject.Find("Map"))
        {
            Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

            foreach(GameObject cp in Checkpoints)
            {
                cp.GetComponent<BoxCollider>().enabled = false;
            }

            Checkpoints[0].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void AddCheckpoint(GameObject checkpoint)
    {
        Checkpoints[Checkpoints.Length] = checkpoint;
    }

    public int GetCurrentCheckpoint()
    {
        return _currentCheckpoint;
    }

    public Vector3 GetCurrentCheckpointPosition()
    {
        return Checkpoints != null && Checkpoints.Length > 0 ? Checkpoints[_currentCheckpoint].transform.position : new Vector3(0, 0, 0);
    }

    public bool IsCheckpointComplete()
    {
        return Checkpoints[_currentCheckpoint].GetComponent<Checkpoint>().IsCheckpointComplete();
    }

    public void SetNextCheckpointActive()
    {
        if (_currentCheckpoint < Checkpoints.Length-1)
        {
            Debug.Log("Player was at checkpoint " + _currentCheckpoint);
            Checkpoints[_currentCheckpoint].GetComponent<Checkpoint>().CompleteCheckpoint();

            _currentCheckpoint++;
            Checkpoints[_currentCheckpoint].GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void UpdateUI()
    {
        _destinationDisplayManager.SetCheckpoint(_currentCheckpoint);
        _narrativeManager.DisplayCheckpointNarrative(_currentCheckpoint);
        _checkpointCounterManager.SetCheckpoint(_currentCheckpoint);
    }
}
