using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject[] Checkpoints;

    [SerializeField]
    private int _currentCheckpoint;

    private NarrativeManager _narrativeManager;

    // Start is called before the first frame update
    void Start()
    {
        _narrativeManager = GameObject.Find("Managers").GetComponent<NarrativeManager>();

        if (!_narrativeManager)
        {
            Debug.LogError("NarrativeManager component not found.");
        }

        if (GameObject.Find("Map"))
        {
            Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        }
    }

    // Update is called once per frame
    void Update()
    {        
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
        if (Checkpoints.Length > 0)
        {
            return Checkpoints[_currentCheckpoint].transform.position;
        }

        return new Vector3(0, 0, 0);
    }

    public bool IsCheckpointComplete()
    {
        return Checkpoints[_currentCheckpoint].GetComponent<Checkpoint>().IsCheckpointComplete();
    }

    public void SetNextCheckpointActive()
    {
        if (_currentCheckpoint < Checkpoints.Length)
        {
            Debug.Log("Player was at checkpoint " + _currentCheckpoint);
            Checkpoints[_currentCheckpoint].GetComponent<Checkpoint>().CompleteCheckpoint();

            _currentCheckpoint++;
            Checkpoints[_currentCheckpoint].GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void DisplayNarrative()
    {
        _narrativeManager.DisplayCheckpointNarrative(_currentCheckpoint);
    }
}
