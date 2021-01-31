using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject[] Checkpoints;

    [SerializeField]
    private int _currentCheckpoint = 0;

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

            foreach(GameObject cp in Checkpoints)
            {
                cp.GetComponent<BoxCollider>().enabled = false;
            }

            Checkpoints[0].gameObject.GetComponent<BoxCollider>().enabled = true;
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

    public void DisplayNarrative()
    {
        _narrativeManager.DisplayCheckpointNarrative(_currentCheckpoint);
    }
}
