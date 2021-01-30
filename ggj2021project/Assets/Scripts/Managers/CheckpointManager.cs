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
        Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        
        if (Checkpoints.Length == 0)
        {
            Debug.LogError("No Checkpoints found!");
        }
        else
        {
            Checkpoints[0].GetComponent<BoxCollider>().enabled = true;
        }

        _narrativeManager = GameObject.Find("Managers").GetComponent<NarrativeManager>();

        if (!_narrativeManager)
        {
            Debug.LogError("NarrativeManager component not found.");
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

    public void DisplayNarrative()
    {
        _narrativeManager.DisplayCheckpointNarrative(_currentCheckpoint);
    }

    public void SetNextCheckpointActive()
    {
        if (_currentCheckpoint < Checkpoints.Length)
        {
            Debug.Log("Player was at checkpoint " + _currentCheckpoint);
            _currentCheckpoint++;
            Checkpoints[_currentCheckpoint].GetComponent<BoxCollider>().enabled = true;
        }
    }
}