using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private int _narrative;
    [SerializeField]
    private bool _isComplete = false;

    private CheckpointManager _checkpointManager;

    // Start is called before the first frame update
    void Start()
    {
        _checkpointManager = GameObject.Find("Managers").GetComponent<CheckpointManager>();

        if (!_checkpointManager)
        {
            Debug.LogError("CheckpointManager component not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_isComplete)
            {
                _isComplete = true;
                _checkpointManager.DisplayNarrative();
                _checkpointManager.SetNextCheckpointActive();
                GetComponent<BoxCollider>().enabled = false;                
            }
        }
    }
}
