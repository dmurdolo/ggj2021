using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] Checkpoints;

    [SerializeField]
    private int _currentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public int GetCurrentCheckpoint()
    {
        return _currentCheckpoint;
    }
}
