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
        GameObject managers = GameObject.Find("Managers");

        if (managers)
        {
            _checkpointManager = managers.GetComponent<CheckpointManager>();

            if (!_checkpointManager)
            {
                Debug.LogError("CheckpointManager component not found.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public bool IsCheckpointComplete()
    {
        return _isComplete;
    }

    public void CompleteCheckpoint()
    {
        _isComplete = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_isComplete)
            {
                _isComplete = true;
                GetComponent<AudioSource>().Play();
                _checkpointManager.UpdateUI();
                _checkpointManager.SetNextCheckpointActive();
                GetComponent<BoxCollider>().enabled = false;                
            }
        }
    }
}
